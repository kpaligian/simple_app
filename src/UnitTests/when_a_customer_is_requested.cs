using Simple.Web.Windsor.Owin.Resources;
using FakeItEasy;
using Simple.Web;
using NUnit.Framework;

namespace UnitTests
{
    public class when_a_customer_is_requested
    {
        GetIndex handler;
        DataService fakeDataService;
        Status status;

        public when_a_customer_is_requested()
        {
            fakeDataService = A.Fake<DataService>();
            //A.CallTo(() => fakeDataService.AddMessage()).MustHaveHappened();
            
            handler = new GetIndex(fakeDataService);
        }

        public void It_should_include_the_correct_details()
        {
            status = handler.Get();
            Assert.IsNotNull(status);
        }
    }
}
