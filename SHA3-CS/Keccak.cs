using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SHA3_CS.Tests")]
namespace SHA3_CS {

	public class Keccak {

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

		private readonly int b, w, l;

		public Keccak(int b = 1600) => (w, l) = GetPermutationWL(this.b = b);

		protected virtual B3D NewSponge() => new Sponge(5, 5, w);

		internal B3D θ(B3D S){
			var C = new Surface(S.Length, S.Width);
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++) C[x,z] = S.Column(x,z).Σ();
			var D = new Surface(S.Length, S.Width);
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++) D[x,z] = C[x-1, z]^C[x+1,z-1];
			B3D s = S.ClearCopy();
			for(int x = 0; x < S.Length; x++) for(int z = 0; z < S.Width; z++){
				var d = D[x,z];
				for(int y = 0; y < S.Height; y++) s[x,y,z] = S[x,y,z]^d;
			}
			return s;//OK!
		}

		internal B3D ρ(B3D S){
			B3D s = S.ClearCopy();
			S.Lane(0, 0).CopyTo(s.Lane(0, 0));
			int x = 1, y = 0;
			for(int t = 0; t < 24; t++){
				for(int z = 0; z < S.Width; z++) s[x,y,z] = S[x,y,z-(t+1)*(t+2)/2];
				(x, y) = (y, (2*x+3*y).mod(S.Height));
			}
			return s;//OK!
		}

		internal B3D π(B3D S){
			B3D s = S.ClearCopy();
			for(int z = 0; z < S.Width; z++) for(int x = 0; x < S.Length; x++) for(int y = 0; y < S.Height; y++) s[x,y,z] = S[x+3*y,x,z];
			return s;//OK!
		}

		internal B3D χ(B3D S){
			B3D s = S.ClearCopy();
			foreach(var (sR, dr) in S.Rows(s)) for(int x = 0; x < sR.Length; x++) dr[x] = sR[x]^( (!sR[x+1]) & sR[x+2] );
			return s;//OK!
		}

		internal B3D ι(B3D S, int Ir){
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

			B3D s = S.Copy();
			var RC = BitString.oS(w);
			for(int j = 0; j <= l; j++) RC = RC.Set((int) Math.Pow(2, j) - 1, rc(j + 7*Ir));
			for(int z = 0; z < S.Width; z++) s[0,0,z] = S[0,0,z]^RC[z];
			return s;//OK!
		}

		internal B3D Rnd(B3D S, int Ir) => ι(χ(π(ρ(θ(S)))), Ir);

		internal B3D Keccak_p(B3D S, int Nr){
			for(int i = 0; i < Nr; i++) S = Rnd(S,i);
			return S;
		}

		internal B3D Keccak_f(B3D S) => Keccak_p(S, 12+2*l);

		public SpongeConstructor Keccak_c(int c) => new KSpongeConstructor(this, c);

		internal class KSpongeConstructor : SpongeConstructor {

			private readonly Keccak keccak;

			internal KSpongeConstructor(Keccak keccak, int c) : base(keccak.b-c) => this.keccak = keccak;

			protected override BitString FMap(BitString S) => keccak.Keccak_f(keccak.NewSponge().ReadFromBitString(S)).ToBitString();
			protected override BitString Padding(int x, int m) => BitString.S1+BitString.oS((-m-2).mod(x))+BitString.S1;
			protected override int b => keccak.b;

		}

	}

	public abstract class SpongeConstructor {

		protected abstract BitString FMap(BitString S);
		protected abstract BitString Padding(int x, int m);
		protected abstract int b {get;}
		protected readonly int r;

		protected SpongeConstructor(int r) => this.r = r;

		public virtual BitString Process(BitString N, int d){
			//Padding - OK!
			var P = N+Padding(r,N.Length);
			var n = P.Length/r;
			var c = b-r;
			//Absorbing - OK!
			BitString Pi(int i) => P.Trunc(i*r,r);
			var S = BitString.oS(b);
			for(int i = 0; i < n; i++) S = FMap(S^(Pi(i)+BitString.oS(c)));
			//Squeezing - OK!
			var Z = BitString.oS(0);
			while(true){
				Z = Z+S.Trunc(r);
				if(d <= Z.Length) return Z.Trunc(d);
				S = FMap(S);
			}
		}

	}

}