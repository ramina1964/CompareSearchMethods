using System.Collections;
using NUnit.Framework;
using CompareSearchMethods.Model;
using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.Test
{
	[TestFixture]
	public class LinearSearchTests
	{
		[TestCase(0), TestCase(1), TestCase(2), TestCase(10), TestCase(20), TestCase(50), TestCase(100), TestCase(1000), TestCase(2000)]
		public void Should_find_correct_index_with_linear_search(int index)
		{
			// Arrange
			const int noOfEntries = (int)1E5;
			var searchItem = new SearchItem();
			var linearSearch = new LinearSearch(searchItem, noOfEntries);
			linearSearch.InitializeData(linearSearch);
			var expected = index;
			var value = linearSearch.ElementAt(index);

			// Act
			var sut = linearSearch.FindItem(value);
			var actual = sut.TargetIndex;

			// Assert 
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
