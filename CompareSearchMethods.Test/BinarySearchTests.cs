using CompareSearchMethods.Model;
using NUnit.Framework;

namespace CompareSearchMethods.Test
{
	[TestFixture]
	public class BinarySearchTests
	{
		[TestCase(0), TestCase(1), TestCase(2), TestCase(10), TestCase(20), TestCase(50), TestCase(100), TestCase(1000), TestCase(2000)]
		public void Should_find_correct_index_with_binary_search(int index)
		{
			// Arrange
			const int noOfEntries = (int)1E5;
			var searchItem = new SearchItem();
			var binarySearch = new BinarySearch(searchItem, noOfEntries);
			binarySearch.InitializeData(binarySearch);
			var expected = index;

			// Act
			var value = binarySearch.ElementAt(index);
			var sut = binarySearch.FindItem(value);
			var actual = sut.TargetIndex;

			// Assert 
			Assert.That(actual, Is.EqualTo(expected));
		}

	}
}
