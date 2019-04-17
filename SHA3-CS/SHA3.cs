using System;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SHA3_CS.Tests")]
namespace SHA3_CS {

	public class SHA3 {

		private static readonly Keccak KECCAK1600 = new Keccak(1600);

		public static readonly SHA3 SHA224 = new SHA3(224);
		public static readonly SHA3 SHA256 = new SHA3(256);
		public static readonly SHA3 SHA384 = new SHA3(384);
		public static readonly SHA3 SHA512 = new SHA3(512);

		public readonly int digestLength;
		private readonly SpongeConstructor constructor;

		public SHA3(int d) => constructor = KECCAK1600.Keccak_c((digestLength = d)*2);

		public BitString Hash(BitString S) => constructor.Process(S+(BitString.S0+BitString.S1), digestLength);
		public BitString Hash(string hexS) => Hash(BitString.FromHexLE(hexS));
		public BitString HashUTF8(string s) => Hash(BitString.FromBytesLE(Encoding.UTF8.GetBytes(s)));

		public string HashHexHex(string hexS) => Hash(hexS).ToHexLE();
		public string HashUTF8Hex(string s) => HashUTF8(s).ToHexLE();

	}

}