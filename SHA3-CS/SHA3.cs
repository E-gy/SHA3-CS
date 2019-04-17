using System;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SHA3_CS.Tests")]
namespace SHA3_CS {

	public static class SHA3 {

		private static Func<BitString, BitString> mkSHA(int digestLength){
			var SHA = Keccak.Keccak_c(digestLength*2);
			return S => SHA(S+BitString.S0+BitString.S1, digestLength);
		}

		public static readonly Func<BitString, BitString> SHA224 = mkSHA(224);
		public static readonly Func<BitString, BitString> SHA256 = mkSHA(256);
		public static readonly Func<BitString, BitString> SHA384 = mkSHA(384);
		public static readonly Func<BitString, BitString> SHA512 = mkSHA(512);

		public static readonly Func<string, string> SHA224UTF8 = s => SHA224(BitString.FromBytesLE(Encoding.UTF8.GetBytes(s))).ToHexLE();
		public static readonly Func<string, string> SHA256UTF8 = s => SHA256(BitString.FromBytesLE(Encoding.UTF8.GetBytes(s))).ToHexLE();
		public static readonly Func<string, string> SHA384UTF8 = s => SHA384(BitString.FromBytesLE(Encoding.UTF8.GetBytes(s))).ToHexLE();
		public static readonly Func<string, string> SHA512UTF8 = s => SHA512(BitString.FromBytesLE(Encoding.UTF8.GetBytes(s))).ToHexLE();
		
	}

}