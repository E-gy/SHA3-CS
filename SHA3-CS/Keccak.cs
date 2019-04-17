using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

[assembly: InternalsVisibleTo("SHA3_CS.Tests")]
namespace SHA3_CS {

	class Keccak {

		private static readonly ImmutableDictionary<int, (int w, int l)> PERMUTATIONS = ImmutableDictionary.ToImmutableDictionary(new Dictionary<int, (int w, int l)>
			{
				{25,	(1,		0)},
				{50,	(2,		1)},
				{100,	(4,		2)},
				{200,	(8,		3)},
				{400,	(16,	4)},
				{800,	(32,	5)},
				{1600,	(64,	6)}
			});

		internal static (int w, int l) GetPermutationWL(int b){
			if(!PERMUTATIONS.ContainsKey(b)) throw new InvalidOperationException($"Keccak - number of bits {b} not supported!");
			return PERMUTATIONS[b];
		}

		internal static Sponge θ(Sponge S){
			var C = new Surface(S.Length, S.Width);
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++) C[x,z] = S.Column(x,z).Σ();
			var D = new Surface(S.Length, S.Width);
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++) D[x,z] = C[x-1, z]^C[x+1,z-1];
			Sponge s = S.ClearCopy();
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++){
				var d = D[x,z];
				for(int y = 0; y < S.Height; y++) s[x,y,z] = S[x,y,z]^d;
			}
			return s;//OK!
		}

		internal static Sponge ρ(Sponge S){
			Sponge s = S.ClearCopy();
			S.Lane(0, 0).CopyTo(s.Lane(0, 0));
			int x = 1, y = 0;
			for(int t = 0; t < 24; t++){
				for(int z = 0; z < S.Width; z++) s[x,y,z] = S[x,y,z-(t+1)*(t+2)/2];
				(x, y) = (y, (2*x+3*y).mod(S.Height));
			}
			return s;//OK!
		}

		internal static Sponge π(Sponge S){
			Sponge s = S.ClearCopy();
			for(int z = 0; z < S.Width; z++) for(int x = 0; x < S.Length; x++) for(int y = 0; y < S.Height; y++) s[x,y,z] = S[x+3*y,x,z];
			return s;//OK!
		}

		internal static Sponge χ(Sponge S){
			Sponge s = S.ClearCopy();
			foreach(var (sR, dr) in S.Rows(s)) for(int x = 0; x < sR.Length; x++) dr[x] = sR[x]^( (!sR[x+1]) & sR[x+2] );
			return s;//OK!
		}

		internal static Sponge ι(Sponge S, (int w, int l) wl, int Ir){
			bool rc(int t){
				if(t.mod(255) == 0) return true;
				var R = BitString.TakeBits(0b10000000, 8);
				for(int i = 1; i <= t.mod(255); i++){
					R = BitString.S0+R;
					var bb = R.Trunc(8).Bits();
					bb[0] = bb[0]^R[8];
					bb[4] = bb[4]^R[8];
					bb[5] = bb[5]^R[8];
					bb[6] = bb[6]^R[8];
					R = new BitString(bb);
				}
				return R[0];
			}

			Sponge s = S.Copy();
			var RC = BitString.oS(wl.w);
			for(int j = 0; j <= wl.l; j++) RC = RC.Set((int) Math.Pow(2, j) - 1, rc(j + 7*Ir));
			for(int z = 0; z < S.Width; z++) s[0,0,z] = S[0,0,z]^RC[z];
			return s;//OK!
		}

		internal static Sponge Rnd(Sponge S, (int w, int l) wl, int Ir) => ι(χ(π(ρ(θ(S)))), wl, Ir);

		internal static Sponge Keccak_p(Sponge S, (int w, int l) wl, int Nr){
			for(int i = 0; i < Nr; i++) S = Rnd(S, wl, i);
			return S;
		}

		internal static Sponge Keccak_f(Sponge S, (int w, int l) wl) => Keccak_p(S, wl, 12+2*wl.l);

		internal static Func<BitString, BitString> Keccak_fBS(int b){
			var wl = GetPermutationWL(b);
			return S => Keccak_f(Sponge.FromBitString(S, 5, 5, wl.w), wl).ToBitString();
		}



		internal static Func<BitString, int, BitString> KSponge(Func<BitString, BitString> f, int b, Func<int, int, BitString> pad, int r) => (N, d) => {
			//Padding - OK!
			var P = N+pad(r,N.Length);
			var n = P.Length/r;
			var c = b-r;
			//Absorbing - OK!
			BitString Pi(int i) => P.Trunc(i*r,r);
			var S = BitString.oS(b);
			for(int i = 0; i < n; i++) S = f(S^(Pi(i)+BitString.oS(c)));
			//Squeezing - OK!
			var Z = BitString.oS(0);
			while(true){
				Z = Z+S.Trunc(r);
				if(d <= Z.Length) return Z.Trunc(d);
				S = f(S);
			}
		};

		internal static BitString pad10x1(int x, int m) => BitString.S1+BitString.oS((-m-2).mod(x))+BitString.S1;

		internal static Func<BitString, int, BitString> Keccak_c(int c, int b = 1600) => KSponge(Keccak_fBS(b), b, pad10x1, b-c);



		internal abstract class B3D {
			public abstract bool this[int i, int j, int k]{ get; set; }
		}

		internal abstract class B2D {
			public abstract int Length { get; }
			public abstract int Width { get; }
			public abstract bool this[int i, int j]{ get; set; }

			public abstract B1D Lane(int x);
			public abstract B1D Row(int z);

			public IEnumerable<B1D> Lanes(){
				for(int x = 0; x < Length; x++) yield return Lane(x);
			}
			public IEnumerable<B1D> Rows(){
				for(int z = 0; z < Width; z++) yield return Row(z);
			}

			public string ToHexLanes() => String.Join("||", Lanes().Select(l => String.Concat(l.ToHex().Reverse()))) + "||";
		}

		internal abstract class B1D {

			public abstract int Length { get; }
			public abstract bool this[int i]{ get; set; }

			public void CopyTo(B1D dest){
				if(this.Length != dest.Length) throw new InvalidOperationException("Can't copy between 2 B1Ds with different lengths");
				for(int i = 0; i < Length; i++) dest[i] = this[i];
			}

			public bool Σ(){
				bool b = false;
				for(int i = 0; i < Length; i++) b ^= this[i];
				return b;
			}

			public string ToHex(){
				var bs = new BitArray(Length);
				for(int i = 0; i < Length; i++) bs[i] = this[i];
				return new BitString(bs).ToHexLE(1);
			}

		}

		class Strink : B1D {

			private bool[] bits;
			public override int Length { get => bits.Length; }

			public Strink(int l) => bits = new bool[l];

			public override bool this[int x]{
				get => bits[x.mod(Length)];
				set => bits[x.mod(Length)] = value;
			}

		}

		class Surface : B2D {

			private bool[,] bits;
			public override int Length { get => bits.GetLength(0); }
			public override int Width { get => bits.GetLength(1); }

			public Surface(int l, int w) => bits = new bool[l, w];

			public override bool this[int x, int z]{
				get => bits[x.mod(Length), z.mod(Width)];
				set => bits[x.mod(Length), z.mod(Width)] = value;
			}
			public bool this[(int x, int z) c]{
				get => this[c.x, c.z];
				set => this[c.x, c.z] = value;
			}

			public override B1D Lane(int x) => new S1D(this, Width, i => (x, i));
			public override B1D Row(int z) => new S1D(this, Length, i => (i, z));

			protected class S1D : B1D {

				private readonly Surface surface;
				private readonly Func<int, (int, int)> indexer;
				public readonly int MaxI;
				public override int Length { get => MaxI; }

				internal S1D(Surface surface, int mI, Func<int, (int, int)> indexer){
					this.surface = surface;
					this.indexer = indexer;
					this.MaxI = mI;
				}

				public override bool this[int i]{
					get => surface[indexer(i)];
					set => surface[indexer(i)] = value;
				}
			}

		}

		internal class Sponge : B3D {

			/**
			 * <summary>
			 * 3D boolean grid storing sponge bits.
			 * Note: Optimized in state-array Keccak referential - so <c>(x=0,y=0,z)</c> in the array corresponds to <c>(floor(Length/2),floor(Height/2),z)</c> in the bottom-left-origin referential; AKA:
			 *	<code>BLO[Xb,Yb,Zb] = Sponge[(Xb-floor(Length/2)).mod(Length), (Yb-floor(Height/2)).mod(Height), Zb]</code>
			 *	and equivalently
			 *	<code>Sponge[Xs,Ys,Zs] = BLO[(Xb+floor(Length/2)).mod(Length), (Yb+floor(Height/2)).mod(Height), Zb]</code>
			 * </summary>
			 */
			private bool[,,] bits;
			public int Length { get => bits.GetLength(0); }
			public int Height { get => bits.GetLength(1); }
			public int Width { get => bits.GetLength(2); }

			public Sponge(int l, int h, int w) => bits = new bool[l,h,w];

			public Sponge ClearCopy() => new Sponge(Length, Height, Width);
			public Sponge Copy(){
				Sponge d = ClearCopy();
				Array.Copy(this.bits, d.bits, Length*Width*Height);
				return d;
			}

			public static Sponge FromBitString(BitString bs, int l, int h, int w){
				var sponge = new Sponge(l, h, w);
				for(int y = 0; y < h; y++) for(int x = 0; x < l; x++) for(int z = 0; z < w; z++) sponge[x,y,z] = bs[w*(h*y + x) + z];
				return sponge;
			}

			public BitString ToBitString(){
				var ba = new BitArray(Length*Width*Height);
				for(int y = 0; y < Height; y++) for(int x = 0; x < Length; x++) for(int z = 0; z < Width; z++) ba[Width*(Height*y + x) + z] = this[x,y,z];
				return new BitString(ba);
			}

			/**
			 * <summary>
			 * Accesses
			 * </summary>
			 */
			public override bool this[int x, int y, int z]{
				get => bits[x.mod(Length), y.mod(Height), z.mod(Width)];
				set => bits[x.mod(Length), y.mod(Height), z.mod(Width)] = value;
			}
			public bool this[(int x, int y, int z) c]{
				get => this[c.x, c.y, c.z];
				set => this[c.x, c.y, c.z] = value;
			}

			public B1D Lane(int x, int y) => new S1D(this, Width, i => (x, y, i));
			public B1D Column(int x, int z) => new S1D(this, Height, i => (x, i, z));
			public B1D Row(int y, int z) => new S1D(this, Length, i => (i, y, z));

			public IEnumerable<B1D> Lanes(){
				for(int x = 0; x < Length; x++) for(int y = 0; y < Height; y++) yield return Lane(x, y);
			}
			public IEnumerable<B1D> Columns(){
				for(int x = 0; x < Length; x++) for(int z = 0; z < Width; z++) yield return Column(x, z);
			}
			public IEnumerable<B1D> Rows(){
				for(int y = 0; y < Height; y++) for(int z = 0; z < Width; z++) yield return Row(y, z);
			}

			public IEnumerable<(B1D src, B1D dest)> Lanes(Sponge dest){
				for(int x = 0; x < Length; x++) for(int y = 0; y < Height; y++) yield return (this.Lane(x, y), dest.Lane(x, y));
			}
			public IEnumerable<(B1D src, B1D dest)> Columns(Sponge dest){
				for(int x = 0; x < Length; x++) for(int z = 0; z < Width; z++) yield return (this.Column(x, z), dest.Column(x, z));
			}
			public IEnumerable<(B1D src, B1D dest)> Rows(Sponge dest){
				for(int y = 0; y < Height; y++) for(int z = 0; z < Width; z++) yield return (this.Row(y, z), dest.Row(y, z));
			}



			public B2D Sheet(int x) => new S2D(this, Height, Width, (i, j) => (x, i, j));
			public B2D Plane(int y) => new S2D(this, Length, Width, (i, j) => (i, y, j));
			public B2D Slice(int z) => new S2D(this, Length, Height, (i, j) => (i, j, z));

			public IEnumerable<B2D> Sheets(){
				for(int x = 0; x < Length; x++) yield return Sheet(x);
			}
			public IEnumerable<B2D> Planes(){
				for(int y = 0; y < Height; y++) yield return Plane(y);
			}
			public IEnumerable<B2D> Slices(){
				for(int z = 0; z < Width; z++) yield return Slice(z);
			}

			public IEnumerable<(B2D src, B2D dest)> Sheets(Sponge dest){
				for(int x = 0; x < Length; x++) yield return (this.Sheet(x), dest.Sheet(x));
			}
			public IEnumerable<(B2D src, B2D dest)> Planes(Sponge dest){
				for(int y = 0; y < Height; y++) yield return (this.Plane(y), dest.Plane(y));
			}
			public IEnumerable<(B2D src, B2D dest)> Slices(Sponge dest){
				for(int z = 0; z < Width; z++) yield return (this.Slice(z), dest.Slice(z));
			}

			public string ToHexSheetsString() => String.Join("\n", Sheets().Select(s => s.ToHexLanes()));

			public override string ToString() => ToHexSheetsString();

			protected class S2D : B2D {

				private readonly Sponge sponge;
				private readonly Func<int, int, (int, int, int)> indexer;
				public readonly int MaxI, MaxJ;
				public override int Length { get => MaxI; }
				public override int Width { get => MaxJ; }

				internal S2D(Sponge sponge, int mI, int mJ, Func<int, int, (int, int, int)> indexer){
					this.sponge = sponge;
					this.indexer = indexer;
					(this.MaxI, this.MaxJ) = (mI, mJ);
				}

				public override bool this[int i, int j]{
					get => sponge[indexer(i, j)];
					set => sponge[indexer(i, j)] = value;
				}

				public override B1D Lane(int i) => new S1D(sponge, MaxJ, j => indexer(i, j));
				public override B1D Row(int j) => new S1D(sponge, MaxI, i => indexer(i, j));

			}

			protected class S1D : B1D {

				private readonly Sponge sponge;
				private readonly Func<int, (int, int, int)> indexer;
				public readonly int MaxI;
				public override int Length { get => MaxI; }

				internal S1D(Sponge sponge, int mI, Func<int, (int, int, int)> indexer){
					this.sponge = sponge;
					this.indexer = indexer;
					this.MaxI = mI;
				}

				public override bool this[int i]{
					get => sponge[indexer(i)];
					set => sponge[indexer(i)] = value;
				}

			}

		}
	}

}