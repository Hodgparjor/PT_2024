using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
