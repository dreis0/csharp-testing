using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking.Mocks
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "mock";
        }
    }
}
