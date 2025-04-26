using System;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class DynArrayTask3_2Tests
    {
        public const int CAPACITY_LESS_THAN_MIN = DynArray<int>.MIN_CAPACITY - 1;
        public const int CAPACITY_GREATER_THEN_MIN = DynArray<int>.MIN_CAPACITY + 1;
        public const int CAPACITY_EQUAL_TO_MIN = DynArray<int>.MIN_CAPACITY;

        [Test]
        public void Constructor_EmptyArgs_InvalidOperationException()
        {
            Assert.Catch<InvalidOperationException>(() => new MultyDynArray<int>());
        }
        
        [TestCase(new [] { 16, 16 }, new [] {17, 17})]
        public void MakeArray_SetNewCapacity_CapacityIncreased(int[] initSize, int[] newSize)
        {
            MultyDynArray<int> array = new MultyDynArray<int>(initSize);
            
            array.MakeArray(newSize);

            int expectedSize = 1;

            foreach (int size in newSize)
            {
                expectedSize *= size;
            }
            
            Assert.That(array.capacity, Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void MakeArray_PassSizeGreaterThenMin_ArraySizeChangedToNewSize()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);
            
            array.MakeArray(new []{CAPACITY_GREATER_THEN_MIN, CAPACITY_GREATER_THEN_MIN});
            
            int expectedSize = CAPACITY_GREATER_THEN_MIN * CAPACITY_GREATER_THEN_MIN;
            
            Assert.That(array.capacity, Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void MakeArray_PassSizeLessThanMin_ArraySizeChangedToMinSize()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(new []{CAPACITY_GREATER_THEN_MIN, CAPACITY_GREATER_THEN_MIN});
            
            array.MakeArray(new []{CAPACITY_LESS_THAN_MIN, CAPACITY_LESS_THAN_MIN});
            
            int expectedSize = CAPACITY_EQUAL_TO_MIN * CAPACITY_EQUAL_TO_MIN;
            
            Assert.That(array.capacity, Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void MakeArray_PassNewValidSizeWhenArrayExistValues_ArrayItemsCopiedToNewArray()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);

            int expectedValue = 1;
            array.Insert(1, 0, 0);
            
            array.MakeArray(CAPACITY_GREATER_THEN_MIN, CAPACITY_GREATER_THEN_MIN);

            int value = array.GetItem(0, 0);
            
            Assert.That(value, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void MakeArray_ResizesFilledArrayToGreaterCapacity_CountNotChanged()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);
            
            array.Insert(1, 0, 0);
            int expectedCount = 1; 
            
            array.MakeArray(CAPACITY_GREATER_THEN_MIN, CAPACITY_GREATER_THEN_MIN);
            
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void MakeArray_ResizesFilledArrayToLowerCapacity_CountIsLimitedToNewCapacity()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_GREATER_THEN_MIN, CAPACITY_GREATER_THEN_MIN);

            for (int i = 0; i < CAPACITY_GREATER_THEN_MIN; i++)
            {
                for (int j = 0; j < CAPACITY_GREATER_THEN_MIN; j++)
                {
                    array.Insert(1, i, j);
                }
            }
            
            array.MakeArray(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);

            int expectedCount = CAPACITY_EQUAL_TO_MIN * CAPACITY_EQUAL_TO_MIN;
            
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [TestCase(new [] {0, -1})]
        [TestCase(new [] {-1, 0})]
        [TestCase(new [] {-1, -1})]
        public void GetItem_IndexOutOfRange_ThrowsIndexOutOfRangeException(int[] index)
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);
            
            Assert.Catch<IndexOutOfRangeException>(() => array.GetItem(index));
        }
        
        [Test]
        public void GetItem_GetExistedValue_ReturnRequiredValue()
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN, CAPACITY_EQUAL_TO_MIN);
            int expectedItem = 5;
            array.Insert(expectedItem, 0, 0);

            int returnedValue = array.GetItem(0, 0);
            
            Assert.That(returnedValue, Is.EqualTo(expectedItem));
        }
        
        [TestCase( -1)]
        [TestCase(1)]
        public void Insert_IndexOutOfRange_ThrowsIndexOutOfRangeException(int index)
        {
            DynArray<int> array = new DynArray<int>();
            
            Assert.Catch<IndexOutOfRangeException>(() => array.Insert(5, index));
        }
        
        [Test]
        public void Insert_PassNewValueWhenArrayIsNotFull_ValueAddedToIndexAndCountIncreased()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_GREATER_THEN_MIN);
            int newCapacity = CAPACITY_GREATER_THEN_MIN * 2;
            array.MakeArray(newCapacity, newCapacity);
            int expectedValue = 5;
            int expectedCount = array.count + 1;
            int expectedIndex = 0;
            
            array.Insert( expectedValue, expectedIndex, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex, expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Insert_PassNewValueWhenArrayIsNotFull_CapacityNotChanged()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_GREATER_THEN_MIN);
            int newCapacity = CAPACITY_GREATER_THEN_MIN * 2;
            array.MakeArray(newCapacity, newCapacity);
            int value = 5;
            int index = 0;
            int expectedCapacity = array.capacity;
            
            array.Insert(value,  index, index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        [Test]
        public void Insert_PassNewValueWhenArrayIsFull_ValueAddedToIndexAndCountIncreased()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_EQUAL_TO_MIN);
            int expectedValue = 5;
            int expectedCount = array.count + 1;
            int expectedIndex = 0;
            
            array.Insert(expectedValue, expectedIndex, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex, expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Insert_PassNewValueWhenArrayIsFull_CapacityDoubled()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_EQUAL_TO_MIN);
            int value = 5;
            int index = 0;
            int expectedCapacity = array.capacity + CAPACITY_EQUAL_TO_MIN;
            
            array.Insert(value, index, index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        [Test]
        public void Insert_EmptyIndexes_InvalidOperationException()
        {
            MultyDynArray<int> array = CreateHalfFilledDynArray2x2Int(CAPACITY_EQUAL_TO_MIN);

            Assert.Catch<InvalidOperationException>(() => array.Insert(5));
        }
        
        [TestCase( -1)]
        [TestCase( 0)]
        [TestCase(1)]
        public void Remove_IndexOutOfRange_ThrowsIndexOutOfRangeException(int index)
        {
            MultyDynArray<int> array = new MultyDynArray<int>(CAPACITY_EQUAL_TO_MIN);
            
            Assert.Catch<IndexOutOfRangeException>(() => array.Remove(index));
        }
        
        [Test]
        public void Remove_WhenFilledPartGreaterThanHalfCapacity_ValueRemovedAtIndexAndCountReduced()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_GREATER_THEN_MIN);
            int expectedIndex = 0;
            int expectedCount = array.count - 1;
            int nextIndex = expectedIndex + 1;
            int expectedValue = array.GetItem(nextIndex, nextIndex);
            
            array.Remove(expectedIndex, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex, expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Remove_WhenFilledPartGreaterThanHalfCapacity_CapacityNotChanged()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_GREATER_THEN_MIN);
            int index = 0;
            int expectedCapacity = array.capacity;
            
            array.Remove(index, index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        [Test]
        public void Remove_WhenFilledPartLessThanHalfCapacity_ValueRemovedAtIndexAndCountReduced()
        {
            MultyDynArray<int> array = CreateFilledDynArray2x2Int(CAPACITY_GREATER_THEN_MIN);
            int newCapacity = CAPACITY_GREATER_THEN_MIN * 2;
            array.MakeArray(newCapacity, newCapacity);
            int expectedIndex = 0;
            int expectedCount = array.count - 1;
            int nextIndex = expectedIndex + 1;
            int expectedValue = array.GetItem(nextIndex, nextIndex);
            
            array.Remove(expectedIndex, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex, expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Remove_WhenFilledPartLessThanHalfCapacity_CapacityReduced()
        {
            MultyDynArray<int> array = CreateHalfFilledDynArray2x2Int(CAPACITY_EQUAL_TO_MIN);
            int newCapacity = CAPACITY_EQUAL_TO_MIN * 2;
            array.MakeArray(newCapacity, newCapacity);
            int index = 0;
            int expectedCapacity = array.capacity - (newCapacity -(int)(newCapacity / DynArray<int>.CAPACITY_DIVIDER));
            
            array.Remove(index, index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        [Test]
        public void Remove_EmptyIndexes_InvalidOperationException()
        {
            MultyDynArray<int> array = CreateHalfFilledDynArray2x2Int(CAPACITY_EQUAL_TO_MIN);

            Assert.Catch<InvalidOperationException>(() => array.Remove());
        }
        
        private MultyDynArray<int> CreateFilledDynArray2x2Int(int count)
        {
            MultyDynArray<int> array = new MultyDynArray<int>(count, count);
            
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    array.Insert(1, i, j);
                }
            }

            return array;
        }
        
        private MultyDynArray<int> CreateHalfFilledDynArray2x2Int(int count)
        {
            MultyDynArray<int> array = new MultyDynArray<int>(count, count);
            
            for (int i = 0; i < count / 2; i++)
            {
                for (int j = 0; j < count / 2; j++)
                {
                    array.Insert(1, i, j);
                }
            }

            return array;
        }
    }
}