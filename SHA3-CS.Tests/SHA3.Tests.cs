using System;
using SHA3_CS;
using NUnit.Framework;

namespace SHA3_CS.Tests {

	class SHA3Tests {
		private static string FormatTestVec(string tv) => tv.Replace(" ", "").ToUpper();

		[Test]
		public void Test224KnownVectors(){
			var SHA = SHA3.SHA224;
			//HEX
			Assert.AreEqual(FormatTestVec("6b4e03423667dbb7 3b6e15454f0eb1ab d4597f9a1b078e3f 5b5a6bc7"), SHA.HashHexHex(""), "SHA3-224 known vec failed");
			Assert.AreEqual(FormatTestVec("e642824c3f8cf24a d09234ee7d3c766f c9a3a5168d0c94ad 73b46fdf"), SHA.HashHexHex("616263"), "SHA3-224 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("6b4e03423667dbb7 3b6e15454f0eb1ab d4597f9a1b078e3f 5b5a6bc7"), SHA.HashUTF8Hex(""), "SHA3-224 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("e642824c3f8cf24a d09234ee7d3c766f c9a3a5168d0c94ad 73b46fdf"), SHA.HashUTF8Hex("abc"), "SHA3-224 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("8a24108b154ada21 c9fd5574494479ba 5c7e7ab76ef264ea d0fcce33"), SHA.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), "SHA3-224 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("543e6868e1666c1a 643630df77367ae5 a62a85070a51c14c bf665cbc"), SHA.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu"), "SHA3-224 UTF-8 known vec failed");
			//Assert.AreEqual(FormatTestVec("d69335b93325192e 516a912e6d19a15c b51c6ed5c15243e7 a7fd653c"), SHA.HashUTF8Hex(new String('a', 1000000)), "SHA3-224 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("55d878a81599f03ec1c19899a967d5c34820e6b603c58d11a5f372c5"), SHA.HashUTF8Hex("OK"), "SHA3-224 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("06ef6fb8d6ef7bfa62571654a34543c3a3178a8e83c749338ab6806f"), SHA.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh."), "SHA3-224 UTF-8 test vec failed");
		}

		[Test]
		public void Test256KnownVectors(){
			var SHA = SHA3.SHA256;
			//HEX
			Assert.AreEqual(FormatTestVec("a7ffc6f8bf1ed766 51c14756a061d662 f580ff4de43b49fa 82d80a4b80f8434a"), SHA.HashHexHex(""), "SHA3-256 known vec failed");
			Assert.AreEqual(FormatTestVec("3a985da74fe225b2 045c172d6bd390bd 855f086e3e9d525b 46bfe24511431532"), SHA.HashHexHex("616263"), "SHA3-256 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("a7ffc6f8bf1ed766 51c14756a061d662 f580ff4de43b49fa 82d80a4b80f8434a"), SHA.HashUTF8Hex(""), "SHA3-256 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("3a985da74fe225b2 045c172d6bd390bd 855f086e3e9d525b 46bfe24511431532"), SHA.HashUTF8Hex("abc"), "SHA3-256 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("41c0dba2a9d62408 49100376a8235e2c 82e1b9998a999e21 db32dd97496d3376"), SHA.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), "SHA3-256 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("916f6061fe879741 ca6469b43971dfdb 28b1a32dc36cb325 4e812be27aad1d18"), SHA.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu"), "SHA3-256 UTF-8 known vec failed");
			//Assert.AreEqual(FormatTestVec("5c8875ae474a3634 ba4fd55ec85bffd6 61f32aca75c6d699 d0cdcb6c115891c1"), SHA.HashUTF8Hex(new String('a', 1000000)), "SHA3-256 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("81e5c554229fce55cac8465a669202b3211540a1fb067f394900de8f5c4c22e1"), SHA.HashUTF8Hex("OK"), "SHA3-256 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("e749a1e72816320f23c9b32209e9cd74ee0c5f34b73d93afd53c8bcbee3511d9"), SHA.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh."), "SHA3-256 UTF-8 test vec failed");
		}

		[Test]
		public void Test384KnownVectors(){
			var SHA = SHA3.SHA384;
			//HEX
			Assert.AreEqual(FormatTestVec("0c63a75b845e4f7d 01107d852e4c2485 c51a50aaaa94fc61 995e71bbee983a2a c3713831264adb47 fb6bd1e058d5f004"), SHA.HashHexHex(""), "SHA3-384 known vec failed");
			Assert.AreEqual(FormatTestVec("ec01498288516fc9 26459f58e2c6ad8d f9b473cb0fc08c25 96da7cf0e49be4b2 98d88cea927ac7f5 39f1edf228376d25"), SHA.HashHexHex("616263"), "SHA3-384 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("0c63a75b845e4f7d 01107d852e4c2485 c51a50aaaa94fc61 995e71bbee983a2a c3713831264adb47 fb6bd1e058d5f004"), SHA.HashUTF8Hex(""), "SHA3-384 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("ec01498288516fc9 26459f58e2c6ad8d f9b473cb0fc08c25 96da7cf0e49be4b2 98d88cea927ac7f5 39f1edf228376d25"), SHA.HashUTF8Hex("abc"), "SHA3-384 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("991c665755eb3a4b 6bbdfb75c78a492e 8c56a22c5c4d7e42 9bfdbc32b9d4ad5a a04a1f076e62fea1 9eef51acd0657c22"), SHA.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), "SHA3-384 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("79407d3b5916b59c 3e30b09822974791 c313fb9ecc849e40 6f23592d04f625dc 8c709b98b43b3852 b337216179aa7fc7"), SHA.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu"), "SHA3-384 UTF-8 known vec failed");
			//Assert.AreEqual(FormatTestVec("eee9e24d78c18553 37983451df97c8ad 9eedf256c6334f8e 948d252d5e0e7684 7aa0774ddb90a842 190d2c558b4b8340"), SHA.HashUTF8Hex(new String('a', 1000000)), "SHA3-384 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("8efc034505a3388cc3baa8b549d55ee2a610ac4d9ba1acf0502e5ac7b249088450206c621b4d7ae698f0171274a2129a"), SHA.HashUTF8Hex("OK"), "SHA3-384 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("f8298d6e62e1613e52be9bee2008a2baab0e3e8ba8b784008debda105dfab954c251dc72786dcee419f9e6fa4d1a3358"), SHA.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh."), "SHA3-384 UTF-8 test vec failed");
		}

		[Test]
		public void Test512KnownVectors(){
			var SHA = SHA3.SHA512;
			//HEX
			Assert.AreEqual(FormatTestVec("a69f73cca23a9ac5 c8b567dc185a756e 97c982164fe25859 e0d1dcc1475c80a6 15b2123af1f5f94c 11e3e9402c3ac558 f500199d95b6d3e3 01758586281dcd26"), SHA.HashHexHex(""), "SHA3-512 known vec failed");
			Assert.AreEqual(FormatTestVec("b751850b1a57168a 5693cd924b6b096e 08f621827444f70d 884f5d0240d2712e 10e116e9192af3c9 1a7ec57647e39340 57340b4cf408d5a5 6592f8274eec53f0"), SHA.HashHexHex("616263"), "SHA3-512 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("a69f73cca23a9ac5 c8b567dc185a756e 97c982164fe25859 e0d1dcc1475c80a6 15b2123af1f5f94c 11e3e9402c3ac558 f500199d95b6d3e3 01758586281dcd26"), SHA.HashUTF8Hex(""), "SHA3-512 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("b751850b1a57168a 5693cd924b6b096e 08f621827444f70d 884f5d0240d2712e 10e116e9192af3c9 1a7ec57647e39340 57340b4cf408d5a5 6592f8274eec53f0"), SHA.HashUTF8Hex("abc"), "SHA3-512 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("04a371e84ecfb5b8 b77cb48610fca818 2dd457ce6f326a0f d3d7ec2f1e91636d ee691fbe0c985302 ba1b0d8dc78c0863 46b533b49c030d99 a27daf1139d6e75e"), SHA.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"), "SHA3-512 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("afebb2ef542e6579 c50cad06d2e578f9 f8dd6881d7dc824d 26360feebf18a4fa 73e3261122948efc fd492e74e82e2189 ed0fb440d187f382 270cb455f21dd185"), SHA.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu"), "SHA3-512 UTF-8 known vec failed");
			//Assert.AreEqual(FormatTestVec("3c3a876da14034ab 60627c077bb98f7e 120a2a5370212dff b3385a18d4f38859 ed311d0a9d5141ce 9cc5c66ee689b266 a8aa18ace8282a0e 0db596c90b0a7b87"), SHA.HashUTF8Hex(new String('a', 1000000)), "SHA3-512 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("affe929e0a5fc0d9c56b693e6918c2d85bb6bbae329e57be43923ac5ad38b2d199cc93f779113924baf20b9b01b9057daefcac56d49ff4a8fbbfe662e5d6a8ce"), SHA.HashUTF8Hex("OK"), "SHA3-512 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("2e0bb986b383115f83e6a2cc0d757e820dda9ea155113eab65500cd903ea9415c79ec5042d6708525bc0adc6a3d840f347c3d8bd5ded37aa9000f84772c8debb"), SHA.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh."), "SHA3-512 UTF-8 test vec failed");
		}

	}

}