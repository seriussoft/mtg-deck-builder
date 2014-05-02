using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;

namespace Utils
{
  [DebuggerStepThrough]
  [DebuggerNonUserCode]
  public static class StringAssertions
  {
    public static void Contains(this IAssertion assertion, string value, string substring, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert.Contains(value, substring);
      }
      else
      {
        StringAssert.Contains(value, substring, message);
      }
    }

    public static void Matches(this IAssertion assertion, string value, Regex pattern, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert.Matches(value, pattern);
      }
      else
      {
        StringAssert.Matches(value, pattern, message);
      }
    }

    public static void DoesNotMatch(this IAssertion assertion, string value, Regex pattern, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert.DoesNotMatch(value, pattern);
      }
      else
      {
        StringAssert.DoesNotMatch(value, pattern, message);
      }
    }

    public static void StartsWith(this IAssertion assertion, string value, string substring, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert.StartsWith(value, substring);
      }
      else
      {
        StringAssert.StartsWith(value, substring, message);
      }
    }

    public static void EndsWith(this IAssertion assertion, string value, string substring, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert.EndsWith(value, substring);
      }
      else
      {
        StringAssert.EndsWith(value, substring, message);
      }
    }

    /* Template
    public static void AAA(this IAssertion assertion, string expected, string actual, string message = null)
    {
      if (String.IsNullOrEmpty(message))
      {
        StringAssert
      }
      else
      {
        StringAssert
      }
    }
    */
  }
}
