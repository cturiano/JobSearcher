using JobSearcher.Abstract;

namespace JobSearcherTest.Mocks
{
    internal class BindableBaseMock : BindableBase
    {
        #region Properties

        public string Prop1
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Prop2
        {
            get => Get<int>();
            set => Set(value);
        }

        #endregion
    }
}