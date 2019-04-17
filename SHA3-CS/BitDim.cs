using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SHA3_CS.Tests")]
namespace SHA3_CS {

	//Abstract

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

	internal abstract class B3D {

		public abstract int Length { get; }
		public abstract int Height { get; }
		public abstract int Width { get; }

		public abstract bool this[int i, int j, int k]{ get; set; }
		public bool this[(int x, int y, int z) c]{
			get => this[c.x, c.y, c.z];
			set => this[c.x, c.y, c.z] = value;
		}

		protected abstract B3D CCpy();
		public B3D ClearCopy() => CCpy();
		protected abstract B3D Cpy();
		public B3D Copy() => Cpy();

		public abstract B1D Lane(int x, int y);
		public abstract B1D Column(int x, int z);
		public abstract B1D Row(int y, int z);

		public IEnumerable<B1D> Lanes(){
			for(int x = 0; x < Length; x++) for(int y = 0; y < Height; y++) yield return Lane(x, y);
		}
		public IEnumerable<B1D> Columns(){
			for(int x = 0; x < Length; x++) for(int z = 0; z < Width; z++) yield return Column(x, z);
		}
		public IEnumerable<B1D> Rows(){
			for(int y = 0; y < Height; y++) for(int z = 0; z < Width; z++) yield return Row(y, z);
		}

		public IEnumerable<(B1D src, B1D dest)> Lanes(B3D dest){
			for(int x = 0; x < Length; x++) for(int y = 0; y < Height; y++) yield return (this.Lane(x, y), dest.Lane(x, y));
		}
		public IEnumerable<(B1D src, B1D dest)> Columns(B3D dest){
			for(int x = 0; x < Length; x++) for(int z = 0; z < Width; z++) yield return (this.Column(x, z), dest.Column(x, z));
		}
		public IEnumerable<(B1D src, B1D dest)> Rows(B3D dest){
			for(int y = 0; y < Height; y++) for(int z = 0; z < Width; z++) yield return (this.Row(y, z), dest.Row(y, z));
		}



		public abstract B2D Sheet(int x);
		public abstract B2D Plane(int y);
		public abstract B2D Slice(int z);

		public IEnumerable<B2D> Sheets(){
			for(int x = 0; x < Length; x++) yield return Sheet(x);
		}
		public IEnumerable<B2D> Planes(){
			for(int y = 0; y < Height; y++) yield return Plane(y);
		}
		public IEnumerable<B2D> Slices(){
			for(int z = 0; z < Width; z++) yield return Slice(z);
		}

		public IEnumerable<(B2D src, B2D dest)> Sheets(B3D dest){
			for(int x = 0; x < Length; x++) yield return (this.Sheet(x), dest.Sheet(x));
		}
		public IEnumerable<(B2D src, B2D dest)> Planes(B3D dest){
			for(int y = 0; y < Height; y++) yield return (this.Plane(y), dest.Plane(y));
		}
		public IEnumerable<(B2D src, B2D dest)> Slices(B3D dest){
			for(int z = 0; z < Width; z++) yield return (this.Slice(z), dest.Slice(z));
		}



		public string ToHexSheetsString() => String.Join("\n", Sheets().Select(s => s.ToHexLanes()));
		public override string ToString() => ToHexSheetsString();

	}

	//Impl

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

		private bool[,,] bits;
		public override int Length { get => bits.GetLength(0); }
		public override int Height { get => bits.GetLength(1); }
		public override int Width { get => bits.GetLength(2); }

		public Sponge(int l, int h, int w) => bits = new bool[l,h,w];

		protected override B3D CCpy() => ClearCopy();
		public new Sponge ClearCopy() => new Sponge(Length, Height, Width);
		protected override B3D Cpy() => Copy();
		public new Sponge Copy(){
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

		public override bool this[int x, int y, int z]{
			get => bits[x.mod(Length), y.mod(Height), z.mod(Width)];
			set => bits[x.mod(Length), y.mod(Height), z.mod(Width)] = value;
		}

		public override B1D Lane(int x, int y) => new S1D(this, Width, i => (x, y, i));
		public override B1D Column(int x, int z) => new S1D(this, Height, i => (x, i, z));
		public override B1D Row(int y, int z) => new S1D(this, Length, i => (i, y, z));

		public override B2D Sheet(int x) => new S2D(this, Height, Width, (i, j) => (x, i, j));
		public override B2D Plane(int y) => new S2D(this, Length, Width, (i, j) => (i, y, j));
		public override B2D Slice(int z) => new S2D(this, Length, Height, (i, j) => (i, j, z));

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