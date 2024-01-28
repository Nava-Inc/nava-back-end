using Moq;
using Nava.Controllers;
using Nava.Interface;

namespace Tests;

public class Tests
{
    private SearchController _controller;
    private Mock<IMusicRepository> _musicRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}