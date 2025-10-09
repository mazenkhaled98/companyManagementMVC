namespace Demo.presentation.ViewModels.Identity
{
   
        public class RoleViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; } //Admin , Editor , User , ...

            public RoleViewModel()
            {
                Id = Guid.NewGuid().ToString();
            }
        }
    }

