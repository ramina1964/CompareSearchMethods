using CompareSearchMethods.GUI.Model;
using NUnit.Framework;

namespace CompareSearchMethods.Test
{
	[TestFixture]
	public class BinarySearchTests
	{
		[TestCase(0), TestCase(1), TestCase(2), TestCase(10), TestCase(20), TestCase(50), TestCase(100), TestCase(1000), TestCase(2000)]
		public void Should_find_correct_index_when_value_exists(int index)
		{
			// Arrange
			const int noOfEntries = (int)1E5;
			var searchItem = new SearchItem();
			var expectedIndex = index;

			// Act
			var sut = new BinarySearch(searchItem, noOfEntries);
			sut.InitializeData();
			var value = sut[index];
			var actualIndex = sut.FindItem(value).TargetIndex;

			// Assert 
			Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		}
	}
}
