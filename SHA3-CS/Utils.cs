using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

[assembly: InternalsVisibleTo("SHA3-CS.Tests")]
namespace SHA3_CS {

	static class PrimitiveExtensions {
		public static int mod(this int a, int b){
			if(b < 0) return a.mod(-b);
			var res = a%b;
			return res < 0 ? res+b : res;
		}
		public static int ceil2I(this double d) => (int) Math.Ceiling(d);
		public static double log2(this double d) => Math.Log(d, 2);
	}

	public class BitString /*: IEnumerable<bool>*/ {

		public static readonly BitString S0 = new BitString(false), S1 = new BitString(true);

		private readonly BitArray ba;
		public int Length { get => ba.Length; }

		public BitString(BitArray ba) => this.ba = ba;
		private BitString(int len) : this(new BitArray(len)){}
		public BitString(bool[] bits) : this(new BitArray(bits)){}
		public BitString(bool bit) : this(new bool[]{bit}){}

		public static BitString oS(int s) => new BitString(s);
		public static BitString TakeBits(int i, int bits){
			BitArray ba = new BitArray(bits);
			for(int b = bits - 1, nb = 0; b >= 0; b--, nb++) ba[nb] = (((i >> b) & 1) != 0);
			return new BitString(ba);
		}
		public static BitString FromBase64(string b64) => new BitString(new BitArray(Convert.FromBase64String(b64)));
		internal static byte revBits(byte b) => (byte)(((b * 0x80200802ul) & 0x0884422110ul) * 0x0101010101ul >> 32);
		public static BitString FromHexBE(string hex){
			BitArray ba = new BitArray(hex.Length*4);
			int b = 0;
			foreach(char c in hex){
				byte b4 = Convert.ToByte(c.ToString(), 16);
				ba[b++] = ((b4 >> 3) & 1) != 0;
				ba[b++] = ((b4 >> 2) & 1) != 0;
				ba[b++] = ((b4 >> 1) & 1) != 0;
				ba[b++] = ((b4 >> 0) & 1) != 0;
			}
			return new BitString(ba);
		}
		public static BitString FromHexLE(string hex){
			if(hex.Length % 2 != 0) throw new InvalidOperationException("Cannot from hex little endian bit order on non-byte-full string");
			BitArray ba = new BitArray(hex.Length*4);
			int b = 8;
			foreach(char c in hex){
				byte b4 = Convert.ToByte(c.ToString(), 16);
				ba[--b] = ((b4 >> 3) & 1) != 0;
				ba[--b] = ((b4 >> 2) & 1) != 0;
				ba[--b] = ((b4 >> 1) & 1) != 0;
				ba[--b] = ((b4 >> 0) & 1) != 0;
				if(b % 8 == 0) b += 16;
			}
			return new BitString(ba);
		}

		public bool this[int b]{
			get => ba[b];
		}

		public BitString Set(int i, bool b){
			BitString copy = new BitString(new BitArray(ba));
			copy.ba[i] = b;
			return copy;
		}

		public BitString Trunc(int s) => Trunc(0, s);

		public BitString Trunc(int s, int l){
			BitString ret = new BitString(l);
			for(int b = 0; b < l; b++) ret.ba[b] = this[s+b];
			return ret;
		}

		public BitString XOr(BitString other){
			if(this.Length != other.Length) throw new InvalidOperationException("Bitstring XOr - lengths don't match");
			return new BitString(new BitArray(this.ba).Xor(other.ba));
		}
		public static BitString operator ^(BitString a, BitString b) => a.XOr(b);

		public BitString Concat(BitString other){
			BitArray nb = new BitArray(ba);
			nb.Length += other.Length;
			for(int b = 0; b < other.Length; b++) nb[Length+b] = other[b];
			return new BitString(nb);
		}
		public static BitString operator +(BitString a, BitString b) => a.Concat(b);

		public bool[] Bits(){
			bool[] bits = new bool[Length];
			ba.CopyTo(bits, 0);
			return bits;
		}

		public long AsLong(){
			long l = 0;
			for(int b = Math.Min(Length, sizeof(long)*8) - 1, nb = 0; b >= 0; b--, nb++) l |= (this[nb] ? 1L : 0L) << b;
			return l;
		}

		public string ToBase64(){
			byte[] bytes = new byte[(int) Math.Ceiling(Length/8d)];
			ba.CopyTo(bytes, 0);
			return Convert.ToBase64String(bytes);
		}

		public string ToHexBE(){
			var s = new List<char>();
			int getBit(int i) => i < Length && this[i] ? 1 : 0;
			for(int b = 0; b < Length;){
				int b4 = 0;
				b4 |= (getBit(b++) << 3);
				b4 |= (getBit(b++) << 2);
				b4 |= (getBit(b++) << 1);
				b4 |= (getBit(b++) << 0);
				s.Add(BitConverter.ToString(new byte[]{(byte) b4})[1]);
			}
			return new string(s.ToArray());
		}

		public string ToHexLE(){
			var s = new List<char>();
			int getBit(int i) => i < Length && this[i] ? 1 : 0;
			for(int b = 8; b <= Length;){
				int b4 = 0;
				b4 |= (getBit(--b) << 3);
				b4 |= (getBit(--b) << 2);
				b4 |= (getBit(--b) << 1);
				b4 |= (getBit(--b) << 0);
				if(b%8 == 0) b += 16;
				s.Add(BitConverter.ToString(new byte[]{(byte) b4})[1]);
			}
			return new string(s.ToArray());
		}

		/*public IEnumerator<bool> GetEnumerator(){
			return null;//FIXME
		}
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();*/

		public override string ToString() => String.Concat(Bits().Select(b => b ? "1" : "0"));

	}
}