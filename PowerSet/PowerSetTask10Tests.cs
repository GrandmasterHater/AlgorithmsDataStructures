//using System.Collections.Generic;
//using System.Diagnostics; // use for performance test on task3
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures.PowerSet
{
    [TestFixture]
    public class PowerSetTask10Tests
    {
        [Test]
        public void Put_WhenValueIsNotInSet_ValueAddedAndCountChanged()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string expectedValue = "value1";
            int expectedSize = 1;
            
            powerSet.Put(expectedValue);
            
            Assert.That(powerSet.Get(expectedValue), Is.True);
            Assert.That(powerSet.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Put_WhenValueIsInSet_ValueNotAddedAndCountNotChanged()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string expectedValue = "value1";
            
            powerSet.Put(expectedValue);
            
            int expectedSize = powerSet.Size();
            
            powerSet.Put(expectedValue);
            
            Assert.That(powerSet.Get(expectedValue), Is.True);
            Assert.That(powerSet.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Remove_WhenValueIsInSet_ReturnTrueValueRemovedAndCountReduced()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string valueForRemove = "value1";
            int expectedSize = 0;
            
            powerSet.Put(valueForRemove);
            
            bool result = powerSet.Remove(valueForRemove);
            
            Assert.That(result, Is.True);
            Assert.That(powerSet.Get(valueForRemove), Is.False);
            Assert.That(powerSet.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Remove_WhenValueIsNotInSet_ReturnFalseValueNotFoundAndCountNotChanged()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string valueForAdd = "value1";
            string valueForRemove = "value2";
            int expectedSize = 1;
            
            powerSet.Put(valueForAdd);
            
            bool result = powerSet.Remove(valueForRemove);
            
            Assert.That(result, Is.False);
            Assert.That(powerSet.Get(valueForRemove), Is.False);
            Assert.That(powerSet.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Get_WhenValueIsInSet_ReturnTrue()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string value = "value1";
            
            powerSet.Put(value);
            
            Assert.That(powerSet.Get(value), Is.True);
        }
        
        [Test]
        public void Get_WhenValueIsNotInSet_ReturnFalseValueNotFoundAndCountNotChanged()
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            string value = "value1";
            string notExistedValue = "value2";
            
            powerSet.Put(value);
            
            Assert.That(powerSet.Get(notExistedValue), Is.False);
        }
        
        [Test]
        public void Intersection_HasIntersections_ReturnSetOfMatchedValues()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"Data", "Structures", "is", "power!"} );
            
            PowerSet<string> result = powerSet1.Intersection(powerSet2);
            
            Assert.That(result.Equals(powerSet2), Is.True);
        }
        
        [Test]
        public void Intersection_HasNotIntersections_ReturnEmptyPowerSet()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"This", ".is.", "the", "way"});
            int expectedSize = 0;
            
            PowerSet<string> result = powerSet1.Intersection(powerSet2);
            
            Assert.That(result.Size(), Is.EqualTo(expectedSize));
            Assert.That(result.AllValues, Is.Empty);
        }
        
        [Test]
        public void Union_PassPowerSetsWithSameValues_ReturnedPowerSetWithExpectedValuesAndSize()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"This", "is", "the", "way", "of", "a", "real", "force!"} );
            int expectedSize = powerSet1.Size() + powerSet2.Size() - 3;
            
            PowerSet<string> result = powerSet1.Union(powerSet2);
            bool valueIsPresentAtLeastOneSheet = result.AllValues.All(value => powerSet1.Get(value) || powerSet2.Get(value));
            
            Assert.That(result.Size(), Is.EqualTo(expectedSize));
            Assert.That(valueIsPresentAtLeastOneSheet, Is.True);
        }
        
        [Test]
        public void Union_PassPowerSetsWithDifferentValues_ReturnedPowerSetWithExpectedValuesAndSize()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"This", ".is.", "the", "way"});
            int expectedSize = powerSet1.Size() + powerSet2.Size();
            
            PowerSet<string> result = powerSet1.Union(powerSet2);
            bool valueIsPresentAtLeastOneSheet = result.AllValues.All(value => powerSet1.Get(value) || powerSet2.Get(value));
            
            Assert.That(result.Size(), Is.EqualTo(expectedSize));
            Assert.That(valueIsPresentAtLeastOneSheet, Is.True);
        }
        
        [Test]
        public void Union_PassEmptySecondPowerSet_ReturnedFirstPowerSetWithExpectedValuesAndSize()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = new PowerSet<string>();
            int expectedSize = powerSet1.Size() + powerSet2.Size();
            
            PowerSet<string> result = powerSet1.Union(powerSet2);
            bool valueIsPresentAtLeastOneSheet = result.AllValues.All(value => powerSet1.Get(value));
            
            Assert.That(result.Size(), Is.EqualTo(expectedSize));
            Assert.That(valueIsPresentAtLeastOneSheet, Is.True);
        }
        
        [Test]
        public void Union_PassEmptyFirstPowerSet_ReturnedFirstPowerSetWithExpectedValuesAndSize()
        {
            PowerSet<string> powerSet1 = new PowerSet<string>();
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"This", "is", "the", "way"});
            int expectedSize = powerSet1.Size() + powerSet2.Size();
            
            PowerSet<string> result = powerSet1.Union(powerSet2);
            bool valueIsPresentAtLeastOneSheet = result.AllValues.All(value => powerSet2.Get(value));
            
            Assert.That(result.Size(), Is.EqualTo(expectedSize));
            Assert.That(valueIsPresentAtLeastOneSheet, Is.True);
        }
        
        [Test]
        public void Difference_SetsHasDifferentValues_ReturnedPowerSetWithExpectedDifferentValues()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"This", ".is.", "the", "way"});
            
            PowerSet<string> result = powerSet1.Difference(powerSet2);
            
            Assert.That(result.Equals(powerSet1), Is.True);
        }
        
        [Test]
        public void Difference_SetsHasNotDifferentValues_ReturnedFullFirstPowerSet()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"is" , "a", "real", "power!"} );
            PowerSet<string> expectedSet = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures"} );
            
            PowerSet<string> result = powerSet1.Difference(powerSet2);
            
            Assert.That(result.Equals(expectedSet), Is.True);
        }
        
        [Test]
        public void IsSubset_EqualsSets_ReturnTrue()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            
            bool result = powerSet1.IsSubset(powerSet2);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsSubset_SubsetExistInSets_ReturnTrue()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures"} );
            
            bool result = powerSet1.IsSubset(powerSet2);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsSubset_SubsetNotExistInSets_ReturnTrue()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"is" , "a", "real", "force!"} );
            
            bool result = powerSet1.IsSubset(powerSet2);
            
            Assert.That(result, Is.False);
        }

        [Test] 
        public void Equals_EqualSets_ReturnTrue()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            
            Assert.That(powerSet1.Equals(powerSet2), Is.True);
        }
        
        [Test] 
        public void Equals_NotEqualSets_ReturnTrue()
        {
            PowerSet<string> powerSet1 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures", "is" , "a", "real", "power!"} );
            PowerSet<string> powerSet2 = CreatePowerSet(new string[]{"Algorithms", "and", "Data", "Structures"} );
            
            Assert.That(powerSet1.Equals(powerSet2), Is.False);
        }
        
        // Exercise 10, task 3, Reflection:
        // Commented because I'm not sure that the server will allow using the Stopwatch class.
        // On average, local measurements when working with collections of 20,000 elements
        // (excluding the Union test that total use 20,000 items) took about 3 seconds for each operation.
        // I believe such mediocre results are due to the simple method of calculating the hash of strings,
        // which leads to frequent collisions. I assume that algorithms with a more uniform distribution
        // will give a better result.
        /*
        [Test, Ignore("Performance test")]
        public void PowerSet_Performance_Test()
        {
            var set1 = new PowerSet<string>();
            var set2 = new PowerSet<string>();
            int count = 20000;
            
            for (int i = 0; i < count / 2; i++)
            {
                set1.Put("str_" + i);
                set2.Put("str_" + (i + count / 2));
            }

            var stopwatch = Stopwatch.StartNew();
            var union = set1.Union(set2);
            stopwatch.Stop();
            long unionTime = stopwatch.ElapsedMilliseconds;
            
            set1 = new PowerSet<string>();
            set2 = new PowerSet<string>();
            
            for (int i = 0; i < count; i++)
            {
                set1.Put("str_" + i);
                set2.Put("str_" + (i + count / 2));
            }
            
            stopwatch.Restart();
            var intersection = set1.Intersection(set2);
            stopwatch.Stop();
            long intersectionTime = stopwatch.ElapsedMilliseconds;
            
            stopwatch.Restart();
            var difference = set1.Difference(set2);
            stopwatch.Stop();
            long differenceTime = stopwatch.ElapsedMilliseconds;
            
            stopwatch.Restart();
            var isSubset = set1.IsSubset(set2);
            stopwatch.Stop();
            long isSubsetTime = stopwatch.ElapsedMilliseconds;
            
            stopwatch.Restart();
            var isEqual = set1.Equals(set1);
            stopwatch.Stop();
            long isEqualTime = stopwatch.ElapsedMilliseconds;

            Assert.Multiple(() =>
            {
                Assert.That(unionTime, Is.LessThanOrEqualTo(2000), $"Union took too long: {unionTime} sec");
                Assert.That(intersectionTime, Is.LessThanOrEqualTo(2000), $"Intersection took too long: {intersectionTime} sec");
                Assert.That(differenceTime, Is.LessThanOrEqualTo(2000), $"Difference took too long: {differenceTime} sec");
                Assert.That(isSubsetTime, Is.LessThanOrEqualTo(2000), $"IsSubset took too long: {isSubsetTime} sec");
                Assert.That(isEqualTime, Is.LessThanOrEqualTo(2000), $"Equals took too long: {isEqualTime} sec");
            });
        }
        */
        
        private PowerSet<string> CreatePowerSet(string[] values)
        {
            PowerSet<string> powerSet = new PowerSet<string>();
            
            foreach (string value in values)
            {
                powerSet.Put(value);
            }
            
            return powerSet;
        }
    }
}