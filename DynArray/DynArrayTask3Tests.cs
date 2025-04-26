using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class DynArrayTask3Tests
    {
        public const int CAPACITY_LESS_THAN_MIN = DynArray<int>.MIN_CAPACITY - 1;
        public const int CAPACITY_GREATER_THEN_MIN = DynArray<int>.MIN_CAPACITY + 1;
        public const int CAPACITY_EQUAL_TO_MIN = DynArray<int>.MIN_CAPACITY;

        #region MakeArray

        [Test]
        public void MakeArray_PassSizeGreaterThenMin_ArraySizeChangedToNewSize()
        {
            DynArray<int> array = new DynArray<int>();
            
            array.MakeArray(CAPACITY_GREATER_THEN_MIN);
            
            Assert.That(array.capacity, Is.EqualTo(CAPACITY_GREATER_THEN_MIN));
            Assert.That(array.array.Length, Is.EqualTo(CAPACITY_GREATER_THEN_MIN));
        }
        
        [Test]
        public void MakeArray_PassSizeLessThanMin_ArraySizeChangedToMinSize()
        {
            DynArray<int> array = new DynArray<int>();
            
            array.MakeArray(CAPACITY_GREATER_THEN_MIN);
            array.MakeArray(CAPACITY_LESS_THAN_MIN);
            
            Assert.That(array.capacity, Is.EqualTo(CAPACITY_EQUAL_TO_MIN));
            Assert.That(array.array.Length, Is.EqualTo(CAPACITY_EQUAL_TO_MIN));
        }
        
        [Test]
        public void MakeArray_PassNewValidSizeWhenArrayExistValues_ArrayItemsCopiedToNewArray()
        {
            DynArray<int> array = new DynArray<int>();
           
            int[] values = Enumerable.Range(0, CAPACITY_EQUAL_TO_MIN).ToArray();
            Array.Resize(ref values, CAPACITY_EQUAL_TO_MIN);
            int[] expected = new int[CAPACITY_GREATER_THEN_MIN]; 
            values.CopyTo(expected, 0);
            
            array.array = values;
            array.MakeArray(CAPACITY_GREATER_THEN_MIN);
            
            int [] actual = new int[] {1, 2, 3}; //[]
            Array.Copy(actual, 1, actual, 0, 2);
            int a = actual[0];
            
            Assert.That(array.array, Is.EqualTo(expected));
        }
        
        [Test]
        public void MakeArray_ResizesFilledArrayToGreaterCapacity_CountNotChanged()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_EQUAL_TO_MIN);
            int expectedCount = array.count;

            array.MakeArray(CAPACITY_GREATER_THEN_MIN);
            
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void MakeArray_ResizesFilledArrayToLowerCapacity_CountIsLimitedToNewCapacity()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            int expectedCount = CAPACITY_EQUAL_TO_MIN;

            array.MakeArray(CAPACITY_EQUAL_TO_MIN);
            
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }

        #endregion

        #region GetItem

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void GetItem_IndexOutOfRange_ThrowsIndexOutOfRangeException(int index)
        {
            DynArray<int> array = new DynArray<int>();
            
            Assert.Catch<IndexOutOfRangeException>(() => array.GetItem(index));
        }
        
        [Test]
        public void GetItem_GetExistedValue_ReturnRequiredValue()
        {
            DynArray<int> array = new DynArray<int>();
            int expectedItem = 5;
            array.Append(expectedItem);

            int returnedValue = array.GetItem(0);
            
            Assert.That(returnedValue, Is.EqualTo(expectedItem));
        }

        #endregion

        #region Append

        [Test]
        public void Append_PassNewValueWhenArrayIsNotFull_AddValueToEndOfArrayAndCountIncreased()
        {
            DynArray<int> array = new DynArray<int>();
            int expectedValue = 5;
            int expectedCount = array.count + 1;
            
            array.Append(expectedValue);
            int lastIndex = array.count - 1;
            int returnedValue = array.GetItem(lastIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Append_PassNewValueWhenArrayIsNotFull_CapacityNotChanged()
        {
            DynArray<int> array = new DynArray<int>();
            int expectedValue = 5;
            int expectedCapacity = array.capacity;
            
            array.Append(expectedValue);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }

        [Test]
        public void Append_PassNewValueWhenArrayIsFull_AddValueToTheEndOfArrayAndCountIncreased()
        {
            DynArray<int> array = new DynArray<int>();

            int[] values = Enumerable.Range(0, CAPACITY_EQUAL_TO_MIN).ToArray();

            foreach (int value in values)
            {
                array.Append(value);
            }

            int expectedValue = 5;
            int expectedCapacity = array.capacity * DynArray<int>.CAPACITY_MULTIPLIER;
            int expectedCount = array.count + 1;
            
            array.Append(expectedValue);
            int lastIndex = array.count - 1;
            int returnedValue = array.GetItem(lastIndex);
            
            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        [Test]
        public void Append_PassNewValueWhenArrayIsFull_CapacityDoubled()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_EQUAL_TO_MIN);

            int expectedValue = 5;
            int expectedCapacity = array.capacity * DynArray<int>.CAPACITY_MULTIPLIER;
            
            array.Append(expectedValue);
            
            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }

        #endregion
        
        #region Insert
        
        // Exercise 3, task 5.1
        [Test]
        public void Insert_PassNewValueWhenArrayIsNotFull_ValueAddedToIndexAndCountIncreased()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            array.MakeArray(CAPACITY_GREATER_THEN_MIN * 2);
            int expectedValue = 5;
            int expectedCount = array.count + 1;
            int expectedIndex = 0;
            
            array.Insert( expectedValue, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        // Exercise 3, task 5.1
        [Test]
        public void Insert_PassNewValueWhenArrayIsNotFull_CapacityNotChanged()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            array.MakeArray(CAPACITY_GREATER_THEN_MIN * 2);
            int value = 5;
            int index = 0;
            int expectedCapacity = array.capacity;
            
            array.Insert(value,  index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        // Exercise 3, task 5.2
        [Test]
        public void Insert_PassNewValueWhenArrayIsFull_ValueAddedToIndexAndCountIncreased()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_EQUAL_TO_MIN);
            int expectedValue = 5;
            int expectedCount = array.count + 1;
            int expectedIndex = 0;
            
            array.Insert(expectedValue, expectedIndex);
            int returnedValue = array.GetItem(expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        // Exercise 3, task 5.2
        [Test]
        public void Insert_PassNewValueWhenArrayIsFull_CapacityDoubled()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_EQUAL_TO_MIN);
            int value = 5;
            int index = 0;
            int expectedCapacity = array.capacity * DynArray<int>.CAPACITY_MULTIPLIER;
            
            array.Insert(value, index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }

        // Exercise 3, task 5.3
        [TestCase( -1)]
        [TestCase(1)]
        public void Insert_IndexOutOfRange_ThrowsIndexOutOfRangeException(int index)
        {
            DynArray<int> array = new DynArray<int>();
            
            Assert.Catch<IndexOutOfRangeException>(() => array.Insert(5, index));
        }

        #endregion
        
        #region Remove
        
        // Exercise 3, task 5.1
        [Test]
        public void Remove_WhenFilledPartGreaterThanHalfCapacity_ValueRemovedAtIndexAndCountReduced()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            int expectedIndex = 0;
            int expectedCount = array.count - 1;
            int expectedValue = array.GetItem(expectedIndex + 1);
            
            array.Remove(expectedIndex);
            int returnedValue = array.GetItem(expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        // Exercise 3, task 5.1
        [Test]
        public void Remove_WhenFilledPartGreaterThanHalfCapacity_CapacityNotChanged()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            int index = 0;
            int expectedCapacity = array.capacity;
            
            array.Remove(index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        // Exercise 3, task 5.2
        [Test]
        public void Remove_WhenFilledPartLessThanHalfCapacity_ValueRemovedAtIndexAndCountReduced()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            array.MakeArray(CAPACITY_GREATER_THEN_MIN * 2);
            int expectedIndex = 0;
            int expectedCount = array.count - 1;
            int expectedValue = array.GetItem(expectedIndex + 1);
            
            array.Remove(expectedIndex);
            int returnedValue = array.GetItem(expectedIndex);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
            Assert.That(array.count, Is.EqualTo(expectedCount));
        }
        
        // Exercise 3, task 5.2
        [Test]
        public void Remove_WhenFilledPartLessThanHalfCapacity_CapacityReduced()
        {
            DynArray<int> array = CreateFilledDynArrayInt(CAPACITY_GREATER_THEN_MIN);
            array.MakeArray(CAPACITY_GREATER_THEN_MIN * 2);
            int index = 0;
            int expectedCapacity = (int)(array.capacity / DynArray<int>.CAPACITY_DIVIDER);
            
            array.Remove(index);

            Assert.That(array.capacity, Is.EqualTo(expectedCapacity));
        }
        
        // Exercise 3, task 5.3
        [TestCase( -1)]
        [TestCase( 0)]
        [TestCase(1)]
        public void Remove_IndexOutOfRange_ThrowsIndexOutOfRangeException(int index)
        {
            DynArray<int> array = new DynArray<int>();
            
            Assert.Catch<IndexOutOfRangeException>(() => array.Remove(index));
        }

        #endregion

        private DynArray<int> CreateFilledDynArrayInt(int count)
        {
            DynArray<int> array = new DynArray<int>();

            int[] values = Enumerable.Range(0, count).ToArray();

            foreach (int value in values)
            {
                array.Append(value);
            }

            return array;
        }
    }
}