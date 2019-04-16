using System;
using SHA3_CS;
using NUnit.Framework;

namespace SHA3_CS.Tests {
	class PrimitiveExtensionsTests {

		[Test]
		public void TestMod(){
			Assert.AreEqual(0, 0.mod(1), "Mod(0, ?) failed");
			Assert.AreEqual(0, 0.mod(5), "Mod(0, ?) failed");
			Assert.AreEqual(0, 0.mod(1454), "Mod(0, ?) failed");
			Assert.AreEqual(0, 0.mod(-1), "Mod(0, ?) failed");
			Assert.AreEqual(0, 0.mod(-158748), "Mod(0, ?) failed");

			Assert.AreEqual(0, 0.mod(1), "Mod(?, 1) failed");
			Assert.AreEqual(0, 542574.mod(1), "Mod(?, 1) failed");
			Assert.AreEqual(0, (-415864).mod(1), "Mod(?, 1) failed");

			Assert.AreEqual(3, 486163.mod(10), "Arbitrary Mod++ failed");
			Assert.AreEqual(9, 64613259.mod(25), "Arbitrary Mod++ failed");
			Assert.AreEqual(566, 566.mod(1516), "Arbitrary Mod++ failed");
			Assert.AreEqual(0, (-1564).mod(4), "Arbitrary Mod-+ failed");
			Assert.AreEqual(22133, (-1654966).mod(45327), "Arbitrary Mod-+ failed");
			Assert.AreEqual(747, 75832583.mod(-758), "Arbitrary Mod+- failed");
			Assert.AreEqual(34, 1224.mod(-35), "Arbitrary Mod+- failed");
			Assert.AreEqual(1742, (-12144154).mod(-3654), "Arbitrary Mod-- failed");
			Assert.AreEqual(9, (-5414).mod(-17), "Arbitrary Mod-- failed");
		}

		[Test]
		public void TestCeil2I(){
			Assert.AreEqual(0, 0d.ceil2I(), "0 ceil failed");
			Assert.AreEqual(18, 18d.ceil2I(), "int ceil failed");
			Assert.AreEqual(-154654, -154654d.ceil2I(), "-int ceil failed");
			
			Assert.AreEqual(19, 18.9d.ceil2I(), "const ceil failed");
			Assert.AreEqual(-154654, (-154654.1d).ceil2I(), "-const ceil failed");
		}

	}

	class BitStringTests {

		Random rng = new Random();

		private bool nextBool() => rng.Next(10) < 5;

		private bool[] nextFillBits(bool[] bits){
			for(int i = 0; i < bits.Length; i++) bits[i] = nextBool();
			return bits;
		}

		[Test]
		public void TestConstants(){
			Assert.AreEqual(1, BitString.S0.Length, "S0 not 1 bit long");
			Assert.AreEqual(1, BitString.S1.Length, "S1 not 1 bit long");

			Assert.AreEqual(false, BitString.S0[0], "S0 not 0");
			Assert.AreEqual(true, BitString.S1[0], "S1 not 1");
		}

		[Test]
		public void TestOutOfBounds(){
			Assert.Throws(typeof(ArgumentOutOfRangeException), () => BitString.S0[2].ToString(), "Access out of bounds worked - should fail!");
		}

		private void TestOS(int ni){
			var bs = BitString.oS(ni);
			Assert.AreEqual(ni, bs.Length, "oS length incorrect");
			for(int i = 0; i < ni; i++) Assert.AreEqual(false, bs[i], "oS contained non-zero bit");
		}

		[Test]
		public void TestOS0() => TestOS(0);

		[Test]
		[Repeat(100)]
		public void TestOS() => TestOS(rng.Next(1525));

		[Test]
		[Repeat(50)]
		public void TestBoolArr(){
			var bits = nextFillBits(new bool[rng.Next(187)]);
			var bs = new BitString(bits);
			Assert.AreEqual(bits.Length, bs.Length, "Bool[]->BS length did not match");
			for(int i = 0; i < bits.Length; i++) Assert.AreEqual(bits[i], bs[i], "Bool[]->BS bits did not match");

			var rebits = bs.Bits();
			Assert.AreEqual(bits, rebits, "BS->Bool[] did not match");
		}

		[Test]
		[Repeat(100)]
		public void TestSet(){
			var bs = BitString.oS(rng.Next(5, 20));
			int rani = rng.Next(bs.Length);
			var nbs = bs.Set(rani, true);
			Assert.False(bs == nbs, "Immutable BS was modified");
			Assert.AreEqual(false, bs[rani], "Old got modified");
			Assert.AreEqual(true, nbs[rani], "New got modified");
		}

		[Test]
		[Repeat(50)]
		public void TestTrunc(){
			var bits = nextFillBits(new bool[rng.Next(10, 50)]);
			var bs = new BitString(bits);
			int ranl = rng.Next(bs.Length);
			var tbs = bs.Trunc(ranl);
			Assert.False(bs == tbs, "Immutable BS was modified");
			Assert.AreEqual(bits.Length, bs.Length, "Src length modified");
			Assert.AreEqual(ranl, tbs.Length, "Target length doesn't match");
			for(int i = 0; i < ranl; i++) Assert.AreEqual(bs[i], tbs[i], "Bits don't match");
		}

		[Test]
		public void TestXorInvalid() => Assert.Throws(typeof(InvalidOperationException), () => (BitString.oS(5)^BitString.S1).Bits(), "BS Xor worked on different length BSs - should not");

		[Test]
		[Repeat(50)]
		public void TestXor(){
			var ranl = rng.Next(5, 35);
			bool[] b1 = nextFillBits(new bool[ranl]), b2 = nextFillBits(new bool[ranl]);
			var b3 = (new BitString(b1)^new BitString(b2)).Bits();
			Assert.AreEqual(ranl, b3.Length, "Xor output length invalid");
			for(int i = 0; i < ranl; i++) Assert.AreEqual(b1[i]^b2[i], b3[i], "Xor bits failed");
		}

		[Test]
		[Repeat(50)]
		public void TestConcat(){
			bool[] b1 = nextFillBits(new bool[rng.Next(7)]), b2 = nextFillBits(new bool[rng.Next(20)]);
			bool[] expected = new bool[b1.Length + b2.Length];
			Array.Copy(b1, 0, expected, 0, b1.Length);
			Array.Copy(b2, 0, expected, b1.Length, b2.Length);
			bool[] actual = (new BitString(b1)+new BitString(b2)).Bits();
			Assert.AreEqual(expected, actual, "Bit string concatenation failed");
		}

		[Test]
		[Repeat(100)]
		public void TestTakeBitsAndToLong(){
			int next = rng.Next();
			int numBits = rng.Next(16);
			var bs = BitString.TakeBits(next, numBits);
			Assert.AreEqual(numBits, bs.Length, "Invalid bit length");
			for(int b = 0; b < numBits; b++) Assert.AreEqual((next>>b)&1, bs[numBits-1-b] ? 1 : 0, "Taken bits didn't match");

			Assert.AreEqual(next&((1<<numBits) -1), (int) bs.AsLong(), "As long did not match");
		}

		[Test]
		[Repeat(25)]
		public void TestBase64SelfConsistency(){
			bool[] b1 = nextFillBits(new bool[rng.Next(5)*8]);
			Assert.AreEqual(b1, (BitString.FromBase64(new BitString(b1).ToBase64())).Bits(), "Base 64 double conversion failed");
		}

		[Test]
		public void TestHexKnownVectors(){
			Assert.AreEqual(new bool[]{true, false, false, false, true, false, true, false}, BitString.FromHexBE("8A").Bits(), "BS From hex string failed");
			Assert.AreEqual(new bool[]{true, false, false, false, true, false, true, false, true, true, true, true, false, false, false, false}, BitString.FromHexBE("8AF0").Bits(), "BS From hex string failed");
			Assert.AreEqual(new bool[]{true, true, true, true, false, false, false, false}, BitString.FromHexBE("F0").Bits(), "BS From hex string failed");

			Assert.AreEqual("E5", new BitString(new bool[]{true, true, true, false, false, true, false, true}).ToHexBE(), "BS to Hex string failed");
			Assert.AreEqual("D4", new BitString(new bool[]{true, true, false, true, false, true, false, false}).ToHexBE(), "BS to Hex string failed");
			Assert.AreEqual("E5D4", new BitString(new bool[]{true, true, true, false, false, true, false, true, true, true, false, true, false, true, false, false}).ToHexBE(), "BS to Hex sstring failed");

			Assert.AreEqual("E8", new BitString(new bool[]{true, true, true, false, true}).ToHexBE(), "BS [not mul 4] to Hex string failed");
			Assert.AreEqual("C", new BitString(new bool[]{true, true}).ToHexBE(), "BS [not mul 4] to Hex string failed");
		} 

		[Test]
		[Repeat(25)]
		public void TestHexSelfConsistency(){
			bool[] b1 = nextFillBits(new bool[rng.Next(5)*8]);
			Assert.AreEqual(b1, (BitString.FromHexBE(new BitString(b1).ToHexBE())).Bits(), "Base 64 double conversion failed");
		}

	}
}