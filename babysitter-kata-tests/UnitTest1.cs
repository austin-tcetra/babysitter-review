using babysitter_kata;

namespace babysitter_kata_tests;

public class Tests
{
    //Smart use of military time
    private readonly Family famA = new(new[] { 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 15, 15, 15, 15, 20 });
    private readonly Family famB = new (new[] { 16, 16, 16, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12, 12, 12, 12, 8, 8 });
    private readonly Family famC = new (new[] { 15, 15, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 21, 21, 21, 15, 15, 15 });

    //It's an interesting choice to start with the exceptions. I generally work on "core" functionality or happy path, but everyone approaches it differently.
    [Test]
    public void TheBabysitterStartsNoEarlierThanFivePM()
    {
        //Nice use of asserting an exception is thrown
        //bs reminds me of the incredibles short (https://youtu.be/Ybar3Q0Caf4?t=210)
        var ex = Assert.Throws<ArgumentException>(() => famA.CalculatePay(15, 19));
        //Checking the exception message is very fancy. I always wrestle with if it's worth testing the message of an exception, but it does look nice.
        Assert.That(ex.Message, Is.EqualTo("Start Hour must be 5:00 PM or after"));
    }

    [Test]
    public void TheBabysitterEndsNoLaterThanFourAM()
    {
        var ex = Assert.Throws<ArgumentException>(() => famA.CalculatePay(20, 5));
        Assert.That(ex.Message, Is.EqualTo("End Hour must be 4:00 AM or earlier"));
    }

    //Did this test force you to drive functionality?
    [Test]
    public void TheBabysitterCanStartInPMAndEndInAM()
    {
        Assert.DoesNotThrow(() => famA.CalculatePay(17, 4));
    }

    [Test]
    public void StartTimeCannotBeAfterEndTime()
    {
        var ex = Assert.Throws<ArgumentException>(() => famA.CalculatePay(20, 18));
        Assert.That(ex.Message, Is.EqualTo("Start Time cannot be after End Time"));
    }

    //It's interesting how someone's test setup dictates the direction they go. I think I've used a family as a map of hour to rate, and so I didn't have a test like this, but I had other tests for my map implementation...
    [Test]
    public void RatesMustBeSpecifiedForExactly24Hours()
    {
        var ex = Assert.Throws<ArgumentException>(() => { new Family(new[] { 20, 20, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 15, 15, 15, 20 }); });
        Assert.That(ex.Message, Is.EqualTo("Rates must be specified for exactly 24 hours"));
    }

    //You do a good job of having very concise tests. There is not a lot of setup or "noise"
    [Test]
    public void FamilyAPays190ForAFullNightShift()
    {
        var pay = famA.CalculatePay(17, 4);
        Assert.That(pay, Is.EqualTo(190));
    }

    /*
     * It's hard to tell without commit history, but I wonder at if these tests really drove small changes to calculate pay
     * I think if we were "competing" and I was doing the implementation, I could have passed all three families
     * just by doing an if statement and returning the hard coded answer per family (which is kind of cheating)
     *
     * How could we force smaller steps towards calculating a full night for a given family, and then expand that to different families?
     * I think one possible option would be to calculate a single hour, so we don't have to implement the for loop, and then add hours?
     * Maybe a separate test for testing the `if (i==24) barrier?
     * 
     * Usually any if statement can be a reason to write a new test
     */
    [Test]
    public void FamilyBPays140ForAFullNightShift()
    {
        var pay = famB.CalculatePay(17, 4);
        Assert.That(pay, Is.EqualTo(140));
    }

    [Test]
    public void FamilyCPays189ForAFullNightShift()
    {
        var pay = famC.CalculatePay(17, 4);
        Assert.That(pay, Is.EqualTo(189));
    }
}