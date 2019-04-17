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

	class ShakeTests {
		private static string FormatTestVec(string tv) => tv.Replace(" ", "").ToUpper();

		[Test]
		public void Test128_32KnownVectors(){
			var SHAKE = Shake.SHAKE128;
			//HEX
			Assert.AreEqual(FormatTestVec("7f9c2ba4"), SHAKE.HashHexHex("", 32), "SHAKE128-32 known vec failed");
			Assert.AreEqual(FormatTestVec("5881092d"), SHAKE.HashHexHex("616263", 32), "SHAKE128-32 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("7f9c2ba4"), SHAKE.HashUTF8Hex("", 32), "SHAKE128-32 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("5881092d"), SHAKE.HashUTF8Hex("abc", 32), "SHAKE128-32 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("1a96182b"), SHAKE.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", 32), "SHAKE128-32 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("7b6df6ff"), SHAKE.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", 32), "SHAKE128-32 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("bb5d7292"), SHAKE.HashUTF8Hex("OK", 32), "SHAKE128-32 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("1212a88e"), SHAKE.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh.", 32), "SHAKE128-32 UTF-8 test vec failed");
		}

		[Test]
		public void Test128_1024KnownVectors(){
			var SHAKE = Shake.SHAKE128;
			//HEX
			Assert.AreEqual(FormatTestVec("7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef263cb1eea988004b93103cfb0aeefd2a686e01fa4a58e8a3639ca8a1e3f9ae57e235b8cc873c23dc62b8d260169afa2f75ab916a58d974918835d25e6a435085b2badfd6dfaac359a5efbb7bcc4b59d538df9a04302e10c8bc1cbf1a0b3a5120ea"), SHAKE.HashHexHex("", 1024), "SHAKE128-1024 known vec failed");
			Assert.AreEqual(FormatTestVec("5881092dd818bf5cf8a3ddb793fbcba74097d5c526a6d35f97b83351940f2cc844c50af32acd3f2cdd066568706f509bc1bdde58295dae3f891a9a0fca5783789a41f8611214ce612394df286a62d1a2252aa94db9c538956c717dc2bed4f232a0294c857c730aa16067ac1062f1201fb0d377cfb9cde4c63599b27f3462bba4"), SHAKE.HashHexHex("616263", 1024), "SHAKE128-1024 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef263cb1eea988004b93103cfb0aeefd2a686e01fa4a58e8a3639ca8a1e3f9ae57e235b8cc873c23dc62b8d260169afa2f75ab916a58d974918835d25e6a435085b2badfd6dfaac359a5efbb7bcc4b59d538df9a04302e10c8bc1cbf1a0b3a5120ea"), SHAKE.HashUTF8Hex("", 1024), "SHAKE128-1024 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("5881092dd818bf5cf8a3ddb793fbcba74097d5c526a6d35f97b83351940f2cc844c50af32acd3f2cdd066568706f509bc1bdde58295dae3f891a9a0fca5783789a41f8611214ce612394df286a62d1a2252aa94db9c538956c717dc2bed4f232a0294c857c730aa16067ac1062f1201fb0d377cfb9cde4c63599b27f3462bba4"), SHAKE.HashUTF8Hex("abc", 1024), "SHAKE128-1024 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("1a96182b50fb8c7e74e0a707788f55e98209b8d91fade8f32f8dd5cff7bf21f54ee5f19550825a6e070030519e944263ac1c6765287065621f9fcb3201723e3223b63a46c2938aa953ba8401d0ea77b8d26490775566407b95673c0f4cc1ce9fd966148d7efdff26bbf9f48a21c6da35bfaa545654f70ae586ff101314207714"), SHAKE.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", 1024), "SHAKE128-1024 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("7b6df6ff181173b6d7898d7ff63fb07b7c237daf471a5ae5602adbccef9ccf4b37e06b4a3543164ffbe0d0557c02f9b25ad434005526d88ca04a6094b93ee57a55d5ea66e744bd391f8f52baf4e031d9e60e5ca32a0ed162bb89fc908097984548796652952dd4737d2a234a401f4857f3d1866efa736fd6a8f7c0b5d02ab06e"), SHAKE.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", 1024), "SHAKE128-1024 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("bb5d72925dfd921c86390cd92997a6eaca87da424dd5649e71a57396582e3acd4052a9aa6de3c03f8966ac99ddf12479aabc85ddeb82cad09193a179d9d970d982dda55c16d7c46d8a406f84f3143ff62ff764a71b512e0efe88b1de3a6f5b78c75dd67398f6341b26ed26c0cd8e5bc737ec2c9fef3531d5d3b557e1ffd7af25"), SHAKE.HashUTF8Hex("OK", 1024), "SHAKE128-1024 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("1212a88ea1c15b0f8cc4dadcdb3c1f521ca17d1127ad74ba56e11184bd77bdb80ab3e06b05cf8d9e69fc593b739b388121f381af0a1ffcbb36b19173481484af62d1d77736eee4c20bd58cf08d87769341b3bcf2e9127c6460d306506f9aa4188022a6c196f8c723562d26f516b691eca1e6c3df4dc40af7f80a49a2c62c7153"), SHAKE.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh.", 1024), "SHAKE128-1024 UTF-8 test vec failed");
		}

		[Test]
		public void Test256_64KnownVectors(){
			var SHAKE = Shake.SHAKE256;
			//HEX
			Assert.AreEqual(FormatTestVec("46b9dd2b0ba88d13"), SHAKE.HashHexHex("", 64), "SHAKE256-64 known vec failed");
			Assert.AreEqual(FormatTestVec("483366601360a877"), SHAKE.HashHexHex("616263", 64), "SHAKE256-64 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("46b9dd2b0ba88d13"), SHAKE.HashUTF8Hex("", 64), "SHAKE256-64 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("483366601360a877"), SHAKE.HashUTF8Hex("abc", 64), "SHAKE256-64 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("4d8c2dd2435a0128"), SHAKE.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", 64), "SHAKE256-64 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("98be04516c04cc73"), SHAKE.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", 64), "SHAKE256-64 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("7e02f3a7b7e6355f"), SHAKE.HashUTF8Hex("OK", 64), "SHAKE256-64 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("7589b2c1b2e36c90"), SHAKE.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh.", 64), "SHAKE256-64 UTF-8 test vec failed");
		}

		[Test]
		public void Test256_2048KnownVectors(){
			var SHAKE = Shake.SHAKE256;
			//HEX
			Assert.AreEqual(FormatTestVec("46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762fd75dc4ddd8c0f200cb05019d67b592f6fc821c49479ab48640292eacb3b7c4be141e96616fb13957692cc7edd0b45ae3dc07223c8e92937bef84bc0eab862853349ec75546f58fb7c2775c38462c5010d846c185c15111e595522a6bcd16cf86f3d122109e3b1fdd943b6aec468a2d621a7c06c6a957c62b54dafc3be87567d677231395f6147293b68ceab7a9e0c58d864e8efde4e1b9a46cbe854713672f5caaae314ed9083dab4b099f8e300f01b8650f1f4b1d8fcf3f3cb53fb8e9eb2ea203bdc970f50ae55428a91f7f53ac266b28419c3778a15fd248d339ede785fb7f"), SHAKE.HashHexHex("", 2048), "SHAKE256-2048 known vec failed");
			Assert.AreEqual(FormatTestVec("483366601360a8771c6863080cc4114d8db44530f8f1e1ee4f94ea37e78b5739d5a15bef186a5386c75744c0527e1faa9f8726e462a12a4feb06bd8801e751e41385141204f329979fd3047a13c5657724ada64d2470157b3cdc288620944d78dbcddbd912993f0913f164fb2ce95131a2d09a3e6d51cbfc622720d7a75c6334e8a2d7ec71a7cc29cf0ea610eeff1a588290a53000faa79932becec0bd3cd0b33a7e5d397fed1ada9442b99903f4dcfd8559ed3950faf40fe6f3b5d710ed3b677513771af6bfe11934817e8762d9896ba579d88d84ba7aa3cdc7055f6796f195bd9ae788f2f5bb96100d6bbaff7fbc6eea24d4449a2477d172a5507dcc931412"), SHAKE.HashHexHex("616263", 2048), "SHAKE256-2048 known vec failed");
			//UTF8
			Assert.AreEqual(FormatTestVec("46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762fd75dc4ddd8c0f200cb05019d67b592f6fc821c49479ab48640292eacb3b7c4be141e96616fb13957692cc7edd0b45ae3dc07223c8e92937bef84bc0eab862853349ec75546f58fb7c2775c38462c5010d846c185c15111e595522a6bcd16cf86f3d122109e3b1fdd943b6aec468a2d621a7c06c6a957c62b54dafc3be87567d677231395f6147293b68ceab7a9e0c58d864e8efde4e1b9a46cbe854713672f5caaae314ed9083dab4b099f8e300f01b8650f1f4b1d8fcf3f3cb53fb8e9eb2ea203bdc970f50ae55428a91f7f53ac266b28419c3778a15fd248d339ede785fb7f"), SHAKE.HashUTF8Hex("", 2048), "SHAKE256-2048 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("483366601360a8771c6863080cc4114d8db44530f8f1e1ee4f94ea37e78b5739d5a15bef186a5386c75744c0527e1faa9f8726e462a12a4feb06bd8801e751e41385141204f329979fd3047a13c5657724ada64d2470157b3cdc288620944d78dbcddbd912993f0913f164fb2ce95131a2d09a3e6d51cbfc622720d7a75c6334e8a2d7ec71a7cc29cf0ea610eeff1a588290a53000faa79932becec0bd3cd0b33a7e5d397fed1ada9442b99903f4dcfd8559ed3950faf40fe6f3b5d710ed3b677513771af6bfe11934817e8762d9896ba579d88d84ba7aa3cdc7055f6796f195bd9ae788f2f5bb96100d6bbaff7fbc6eea24d4449a2477d172a5507dcc931412"), SHAKE.HashUTF8Hex("abc", 2048), "SHAKE256-2048 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("4d8c2dd2435a0128eefbb8c36f6f87133a7911e18d979ee1ae6be5d4fd2e332940d8688a4e6a59aa8060f1f9bc996c05aca3c696a8b66279dc672c740bb224ec37a92b65db0539c0203455f51d97cce4cfc49127d7260afc673af208baf19be21233f3debe78d06760cfa551ee1e079141d49dd3ef7e182b1524df82ea1cefe1c6c3966175f0228d35887cd9f09b05457f6d952f9b3b32464e0b3c54dcc13efdb4c54e29cdb4088faf482cddd0a5e6b822f5a80d0cc78d4cc90131906fd5159eb5142e155024b62402eb0017f986c9638ba61970e9086dd94884275f484d3c3b8422110ed64f079ab2c9acff78e8bd4951923f75f0a2f18c43806ce5de92386f"), SHAKE.HashUTF8Hex("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq", 2048), "SHAKE256-2048 UTF-8 known vec failed");
			Assert.AreEqual(FormatTestVec("98be04516c04cc73593fef3ed0352ea9f6443942d6950e29a372a681c3deaf4535423709b02843948684e029010badcc0acd8303fc85fdad3eabf4f78cae165635f57afd28810fc22abf63df55c5ead450fdfb64209010e982102aa0b5f0a4b4753b53eb4b5319c06986f5aac5cc247256d06b05a273d7ef8d31864777d488d541451ed82a38926582deb65d40ddb959b79dbe933635f9f3e2ae57f7c6aefc4d5bd7f230070fc2e9e2357d4eb39cee4bd064c4a33f35d5f652774fe941300cce4e800b127d54ba3548986db411d08dee19a295c1e9219e8c76a292bae5cfecf54785b37044bac9deef0f129c666b99719164d5f62ccef52b2ae53e4e8e971646"), SHAKE.HashUTF8Hex("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu", 2048), "SHAKE256-2048 UTF-8 known vec failed");
			//Custom
			Assert.AreEqual(FormatTestVec("7e02f3a7b7e6355fce0dcaa1fa8a625bbf3b20dbe8f19ae1f9cbb77249c43a8af05a12a51172ee68232cf1892ea92c627807109573e35ade45ac50edeaa9014f3b4e636b16ddee747b333cc4c804ee429f8cd0b3ad0b7e43dd27b00126ffda66e29460df676bf16673dca5e39187088a18c30584c6689b9ad0c9637e3cf41c143d53be0e92322d83715b5a529ff594369b309ff56f1cce49e85167a98f1db7c112feec840cbd85cd8c4a80e3759899fae9965792e428a23f8a086d5ce9b379895d45e29c5d61128ac8732cc510e3e6b9a5c89e6edd0728ee123d9815b4c80c9af2c972102668e9a6ad038c0af5dd80be4166f130d00ed2547fe2bf6ea5a0cce7"), SHAKE.HashUTF8Hex("OK", 2048), "SHAKE256-2048 UTF-8 test vec failed");
			Assert.AreEqual(FormatTestVec("7589b2c1b2e36c9001b16eedf0644475ad7c91fa7efa5af06aa3cf454ec5c0a6eb68797bc6ce54687a9ce25dd6ace2b86f84416de4b81c247e64747e35847ecbb69e3a729e53b52c0e6bce889427410c0013418128481994f2cf4fa2e4e7ee5dff95a35bf3e1c3a5ec3814fb250e428ee102275da370941d09a34e853566145362b0e2c1767f1207b1425a5711f425801e226e45b8e76a7eb21f37c68e9532cf733abc160f32ebe8a3ff775896f53edcc70b44f1744d79bc0c11bc59476194b9d3d2b90d3b261ea7b339af11ed5a3821c0e52a3af8fe3b93d82f7648eaddd90a60c2074ece2bbec00a8d8049a0ea5bdc5f6b2bf20a13c0949bb8e59d220d1e57"), SHAKE.HashUTF8Hex("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dapibus vel erat in eleifend. Cras laoreet sed lorem vel luctus. Praesent ullamcorper volutpat sem at feugiat. Praesent in posuere leo. Curabitur sollicitudin odio vel urna pulvinar, ut tristique leo placerat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Aliquam vel felis eu lorem blandit commodo vel vitae mauris. Sed eget massa eu felis tincidunt maximus non ac metus. Aenean eu eros sit amet sem tristique tristique. Sed sit amet augue cursus, sollicitudin felis non, tincidunt nibh.", 2048), "SHAKE256-2048 UTF-8 test vec failed");
		}

	}

}