using VideocartLab.Models;

namespace VideocartLab.Tests;

public class ModelsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MaxConnectionTest()
    {
        Connector connector = new Connector();

        for (int i = 0; i < 7; i++)
        {
            connector.TargetConnections.Add(new Connector());
        }

        int count1 = connector.TargetConnections.Count;

        connector.MaxConnectionCount = 3;

        int count2 = connector.TargetConnections.Count;

        Assert.AreEqual(count2, 3);
    }

}
