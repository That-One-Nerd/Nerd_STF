namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class ValidatableTests
{
    [TestMethod] public void TestValidatable()
    {
        int counter = 0;
        Validatable<object?> obj = new(validate);

        // Must start invalid.
        Assert.IsFalse(obj.Validated);
        Assert.AreEqual(0, counter);

        // Should call the validate() function exactly once.
        object? result = obj.Value;
        Assert.AreEqual(1, counter);
        Assert.AreEqual(counter, result);

        // Calling obj.Value again should not cause validate() to execute again.
        Assert.IsTrue(obj.Validated);
        result = obj.Value;
        Assert.AreEqual(1, counter);
        Assert.AreEqual(counter, result);

        // Invalidating should not immediately call the validate() function yet.
        obj.Invalidate();
        Assert.IsFalse(obj.Validated);
        Assert.AreEqual(1, counter);

        // Now that we're invalid, we should get another validate() call.
        result = obj.Value;
        Assert.IsTrue(obj.Validated);
        Assert.AreEqual(2, counter);
        Assert.AreEqual(counter, result);

        // Accept a parameter. validate() should not be called.
        obj.Invalidate();
        Assert.IsFalse(obj.Validated);
        obj.Accept(-12);
        Assert.IsTrue(obj.Validated);
        result = obj.Value;
        Assert.AreEqual(2, counter);
        Assert.AreEqual(-12, result);

        object? validate() => ++counter;
    }
}
