using Xunit;


public class SolutionTests
{
    private readonly Solution _sut;


    public SolutionTests()
    {
        _sut = new Solution();
    }

    [Theory]
    [InlineData(new int[]{0,1}, new int[]{2,7,11,15}, 9)]
    [InlineData(new int[]{1,2}, new int[]{3,2,4}, 6)]
    [InlineData(new int[]{0,1}, new int[]{3,3}, 6)]
    public void ReturnsRightResult(int[] res, int[] arr, int target)
    {
        Assert.Equal(res, _sut.TwoSum(arr, target));
    }
}