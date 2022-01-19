using NUnit.Framework;
using RomanMath.Impl;
using System;

namespace RomanMath.Tests
{
	public class Tests
	{
		[SetUp]
		public void Init()
		{
		}

		[Test]

		[TestCase("IV+II*V", 14)]
		[TestCase("IV+I*V", 9)]
		[TestCase("IV-V*V", 21)]
		[TestCase("IV+II*V", 14)]
		public void EvaluateExsperession_ShouldReturnValidResult(string inputData, int outPutData)
		{
			var result = Service.Evaluate(inputData);
			Assert.AreEqual(outPutData, result);
		}

		[Test]
		[TestCase("IV+II*V123", 14)]
		public void EvaluateExsperession_ShouldReturnInValidResult(string inputData, int outPutData)
		{
            try
            {
				var result = Service.Evaluate("IV+II*V123");
				Assert.AreEqual(outPutData, result);
			}
			catch(Exception ex)
            {
				if(ex is NotSupportedException)
				{
					Assert.Pass();
				}
            }

		}
	}
}