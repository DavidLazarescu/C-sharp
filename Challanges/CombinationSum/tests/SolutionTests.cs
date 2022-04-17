using System;
using System.Collections.Generic;
using Xunit;

namespace tests;

public class UnitTest1
{
    private Solution _solution = new Solution();


    [Fact]
    public void WorksAsExpected()
    {
        // Arrange
        int[] candidates = new int[] {2,3,5};
        int target = 8;
        var expectedResult = new List<IList<int>>() { new List<int>(){2,2,2,2}, new List<int>(){2,3,3}, new List<int>(){3,5}};

        // Act
        var result = _solution.CombinationSum(candidates, target);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}