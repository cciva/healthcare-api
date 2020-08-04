using System.Collections.Generic;

namespace Contracts
{
    public class ShopRq
    {
        public Basket Basket { get; }
    }
    
    // collection of medicaments patient
    // is ready to purchase
    public class Basket : List<Medicine>
    {
        
    }
}