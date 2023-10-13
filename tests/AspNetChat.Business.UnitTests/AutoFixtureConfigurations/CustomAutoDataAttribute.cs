using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace AspNetChat.Business.UnitTests.AutoFixtureConfigurations
{
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base(CreateFixture)
        { }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
