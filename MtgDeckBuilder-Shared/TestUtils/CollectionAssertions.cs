using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Utils
{
  [DebuggerStepThrough]
  [DebuggerNonUserCode]
  public static class CollectionAssertions
  {
    private static void MethodsLeft()
    {
      //CollectionAssert.ReferenceEquals
    }

    [MethodImplAttribute(MethodImplOptions.NoInlining)] //to prevent inlining from destroying the stacktrace assumption of method stacks
    private static void FailAssert(string message, params object[] parameters)
    {
      var assertionName = new StackTrace().GetFrame(1).GetMethod().Name;
      var formattedMessage = String.Format(message, parameters);
      var finalMessage = String.Format("{0} - {1}", assertionName, formattedMessage);

      throw new AssertFailedException(finalMessage);
    }

    public static void CountIs(this IAssertion assertion, ICollection collection, int count, string message = null)
    {
      if (collection.Count != count)
      {
        if(String.IsNullOrEmpty(message))
        {
          FailAssert("Expected {0} items in collection, but collection actually contained {1} items.", count, collection.Count);
        }
        else
        {
          FailAssert(message);
        }
      }
    }

    public static void CountIsNot(this IAssertion assertion, ICollection collection, int count, string message = null)
    {
      if (collection.Count == count)
      {
        if (String.IsNullOrEmpty(message))
        {
          FailAssert("Did not expect {0} items in collection.", count);
        }
        else
        {
          FailAssert(message);
        }
      }
    }

    public static void IsEmpty(this IAssertion assertion, ICollection collection, string message = null)
    {
      if (collection.Count > 0)
      {
        if (String.IsNullOrEmpty(message))
        {
          FailAssert("Expected empty collection, but collection actually contained {1} items.", collection.Count);
        }
        else
        {
          FailAssert(message);
        }
      }
    }

    public static void IsNotEmpty(this IAssertion assertion, ICollection collection, string message = null)
    {
      if (collection.Count == 0)
      {
        if (String.IsNullOrEmpty(message))
        {
          FailAssert("Expected collection to have items, but collection is actually empty");
        }
        else
        {
          FailAssert(message);
        }
      }
    }

    public static void AllItemsAreInstancesOfType(this IAssertion assertion, ICollection collection, Type expectedType, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType);
      }
      else
      {
        CollectionAssert.AllItemsAreInstancesOfType(collection, expectedType, message);
      }
    }

    public static void AllItemsAreUnique(this IAssertion assertion, ICollection collection, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.AllItemsAreUnique(collection);
      }
      else
      {
        CollectionAssert.AllItemsAreUnique(collection, message);
      }
    }

    public static void AllItemsAreNotNull(this IAssertion assertion, ICollection collection, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.AllItemsAreNotNull(collection);
      }
      else
      {
        CollectionAssert.AllItemsAreNotNull(collection, message);
      }
    }

    public static void Contains(this IAssertion assertion, ICollection collection, object expected, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.Contains(collection, expected);
      }
      else
      {
        CollectionAssert.Contains(collection, expected, message);
      }
    }

    public static void DoesNotContain(this IAssertion assertion, ICollection collection, object expected, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.DoesNotContain(collection, expected);
      }
      else
      {
        CollectionAssert.DoesNotContain(collection, expected, message);
      }
    }

    public static void IsSubsetOf(this IAssertion assertion, ICollection subset, ICollection superset, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.IsSubsetOf(subset, superset);
      }
      else
      {
        CollectionAssert.IsSubsetOf(subset, superset, message);
      }
    }

    public static void IsNotSubsetOf(this IAssertion assertion, ICollection subset, ICollection superset, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.IsNotSubsetOf(subset, superset);
      }
      else
      {
        CollectionAssert.IsNotSubsetOf(subset, superset, message);
      }
    }

    /// <summary>
    /// <para>Two collections are equal if they have the same elements in the same order and quantity.</para>
    /// <para>Elements are equal if their values are equal, not if they refer to the same object.</para>
    /// <para>The values of elements are compared using Equals by default.</para>
    /// </summary>
    /// <param name="assertion"></param>
    /// <param name="expectedCollection"></param>
    /// <param name="actualCollection"></param>
    /// <param name="comparer"></param>
    /// <param name="message"></param>
    public static void AreEqualCollections(this IAssertion assertion, ICollection expectedCollection, ICollection actualCollection, IComparer comparer = null, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        if (comparer != null)
        {
          CollectionAssert.AreEqual(expectedCollection, actualCollection, comparer);
        }
        else
        {
          CollectionAssert.AreEqual(expectedCollection, actualCollection);
        }
      }
      else
      {
        if (comparer != null)
        {
          CollectionAssert.AreEqual(expectedCollection, actualCollection, comparer, message);
        }
        else
        {
          CollectionAssert.AreEqual(expectedCollection, actualCollection, message);
        }
      }
    }

    /// <summary>
    /// <para>Two collections are equal if they have the same elements in the same order and quantity.</para>
    /// <para>Elements are equal if their values are equal, not if they refer to the same object.</para>
    /// <para>The values of elements are compared using Equals by default.</para>
    /// </summary>
    /// <param name="assertion"></param>
    /// <param name="expectedCollection"></param>
    /// <param name="actualCollection"></param>
    /// <param name="comparer"></param>
    /// <param name="message"></param>
    public static void AreNotEqualCollections(this IAssertion assertion, ICollection expectedCollection, ICollection actualCollection, IComparer comparer = null, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        if (comparer != null)
        {
          CollectionAssert.AreNotEqual(expectedCollection, actualCollection, comparer);
        }
        else
        {
          CollectionAssert.AreNotEqual(expectedCollection, actualCollection);
        }
      }
      else
      {
        if (comparer != null)
        {
          CollectionAssert.AreNotEqual(expectedCollection, actualCollection, comparer, message);
        }
        else
        {
          CollectionAssert.AreNotEqual(expectedCollection, actualCollection, message);
        }
      }
    }

    /// <summary>
    /// <para>Two collections are equivalent if they have the same elements in the same quantity, but in any order.</para>
    /// <para>Elements are equal if their values are equal, not if they refer to the same object.</para>
    /// </summary>
    /// <param name="assertion"></param>
    /// <param name="expectedCollection"></param>
    /// <param name="actualCollection"></param>
    /// <param name="message"></param>
    /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">An element was found in one of the collections but not the other.</exception>
    public static void AreEquivalentCollections(this IAssertion assertion, ICollection expectedCollection, ICollection actualCollection, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.AreEquivalent(expectedCollection, actualCollection);
      }
      else
      {
        CollectionAssert.AreEquivalent(expectedCollection, actualCollection, message);
      }
    }

    /// <summary>
    /// <para>Two collections are equivalent if they have the same elements in the same quantity, but in any order.</para>
    /// <para>Elements are equal if their values are equal, not if they refer to the same object.</para>
    /// </summary>
    /// <param name="assertion"></param>
    /// <param name="expectedCollection"></param>
    /// <param name="actualCollection"></param>
    /// <param name="message"></param>
    /// <exception cref="Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException">The two collections contain the same elements, including the same number of duplicate occurrences of each element.</exception>
    public static void AreNotEquivalentCollections(this IAssertion assertion, ICollection expectedCollection, ICollection actualCollection, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert.AreNotEquivalent(expectedCollection, actualCollection);
      }
      else
      {
        CollectionAssert.AreNotEquivalent(expectedCollection, actualCollection, message);
      }
    }

    /* Template
    public static void AAA(this IAssertion assertion, ICollection collection, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        CollectionAssert
      }
      else
      {
        CollectionAssert
      }
    }
    */
  }
}
