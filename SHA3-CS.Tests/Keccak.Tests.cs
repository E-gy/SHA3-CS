using System;
using System.Collections.Generic;
using SHA3_CS;
using NUnit.Framework;

namespace SHA3_CS.Tests {

	class SpongeTests {

		Random rng = new Random();

		[Test]
		[Repeat(10)]
		public void TestMutabilityAndView(){
			var S = new Keccak.Sponge(rng.Next(1, 10), rng.Next(1, 10), rng.Next(1, 100));
			void assertIntegrity(){
				for(int x = 0; x < S.Length; x++) for(int y = 0; y < S.Height; y++) for(int z = 0; z < S.Width; z++){
					var exp = S[x,y,z];
					Assert.AreEqual(exp, S.Sheet(x)[y,z], "Sheet view not working");
					Assert.AreEqual(exp, S.Plane(y)[x,z], "Plane view not working");
					Assert.AreEqual(exp, S.Slice(z)[x,y], "Slice view not working");
					Assert.AreEqual(exp, S.Lane(x,y)[z], "Lane view not working");
					Assert.AreEqual(exp, S.Column(x,z)[y], "Column view not working");
					Assert.AreEqual(exp, S.Row(y,z)[x], "Row view not working");
				}
			}
			IEnumerable<(int x, int y, int z)> rndPos(int amount){
				for(int i = 0; i < rng.Next(S.Length*S.Height*S.Width/4); i++) yield return (rng.Next(S.Length), rng.Next(S.Height), rng.Next(S.Width));
			}

			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S[x,y,z] = !S[x,y,z];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Sheet(x)[y, z] = !S.Sheet(x)[y, z];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Plane(y)[x, z] = !S.Plane(y)[x, z];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Slice(z)[x, y] = !S.Slice(z)[x, y];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Lane(x, y)[z] = !S.Lane(x, y)[z];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Column(x, z)[y] = !S.Column(x, z)[y];
			assertIntegrity();
			foreach(var (x,y,z) in rndPos(S.Length*S.Height*S.Width/4)) S.Row(y, z)[x] = !S.Row(y, z)[x];
			assertIntegrity();
		}

	}

}