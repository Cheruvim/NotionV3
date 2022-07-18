namespace Services.UseCases 
{
    using DateBaseServices;
    using DateBaseServices.Models;

    public class UserController 
    {
        public void AddOrUpdateUser(User user)
        {
            if (user.UserId < 1){
                AddUser(user);
            }
            else{
                UpdateUser(user);
            }
        }
        
        private void AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
                throw new UseCaseException();
            
            if (string.IsNullOrEmpty(user.Password))
                throw new UseCaseException();
            
            if (string.IsNullOrEmpty(user.Name))
                throw new UseCaseException();
            
            using (var db = new DataContext())
            {
                
            }


        }
        
        private void UpdateUser(User user)
        {
            if()
        }
        
    }
}
