using Presentation.ViewModel;


namespace PresentationTests
{
    internal interface IModelGenerator
    {
        void GenerateUserModels(UserMasterVM viewModel);

        void GenerateProductModels(ProductMasterVM viewModel);

        void GenerateWarehouseEntryModels(WarehouseEntryMasterVM viewModel);

        void GenerateEventModels(EventMasterVM viewModel);
    }
}
