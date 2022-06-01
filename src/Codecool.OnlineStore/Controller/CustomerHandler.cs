namespace Codecool.OnlineStore.Controller
{
    enum CustomerOptions
    {
        DisplayAvailableProducts = 0,
        DisplayProductsFromCategory = 1,
        DisplayAllInCart = 2,
        SubmitOrder = 3,
        ManageCart = 4,
        DisplayPreviousOrders = 5,
        RateProduct = 6,
        ShowStats = 7

    }

    public class CustomerHandler : BaseHandler
    {
        private readonly Customer _user;
        private readonly ProductHandler _productHandler;

        public CustomerHandler(IFactory<UnitOfWork> unitOfWorkFactory, IInputSystem inputManager, IView view, IMenuDisplay display, int customerId, ProductHandler productHandler)
            : base(unitOfWorkFactory, inputManager, view, display)
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            _user = unitOfWork.Users.GetFirstOrDefaultWithShoppingCart(x => x.UserId == customerId);
            unitOfWork.CompleteUnit();

            _availableCommands = new string[] { "Display all available products", "Display products from selected category", "Display all items in your cart", "Submit order", "Manage cart", "Display previous orders", "Rate product", "Show statistics" };
            _productHandler = productHandler;
        }

        public override void RunFeatureBasedOn(int userInput)
        {
            switch (userInput)
            {
                case (int)CustomerOptions.DisplayAvailableProducts:
                    DisplayAvailableProducts();
                    break;
                case (int)CustomerOptions.DisplayProductsFromCategory:
                    DisplayProductsFromCategory();
                    break;
                case (int)CustomerOptions.DisplayAllInCart:
                    DisplayAllInCart();
                    break;
                case (int)CustomerOptions.SubmitOrder:
                    SubmitOrder();
                    break;
                case (int)CustomerOptions.ManageCart:
                    ManageCart();
                    break;
                case (int)CustomerOptions.DisplayPreviousOrders:
                    DisplayPreviousOrder();
                    break;
                case (int)CustomerOptions.RateProduct:
                    RateProduct();
                    break;
                case (int)CustomerOptions.ShowStats:
                    ShowStats();
                    break;
                default:
                    break;
            }
        }

        private void DisplayAvailableProducts()
        {
            var availableProducts = GetAllAvailableProducts();
            if (!availableProducts.Any())
            {
                _display.PrintMessage("There are no available products");
                return;
            }
            _view.DisplayAll(availableProducts);
            var choosenProduct = ChooseProduct(availableProducts);
            AddItemToCart(choosenProduct);
        }

        private List<Product> GetAllAvailableProducts()
        {
            using (var unitOfWork = _unitOfWorkFactory.GetNew())
            {
                var availableProducts = new List<Product>();
                var allProducts = unitOfWork.Products.GetAll().ToList();
                foreach (var product in allProducts)
                {
                    if (product.IsAvailable == true)
                        availableProducts.Add(product);
                }
                unitOfWork.CompleteUnit();
                return availableProducts;
            }
        }
        private void DisplayProductsFromCategory()
        {
            var productList = _productHandler.GetAllProductsFromSpecifiedCategory();
            List<string> productNames = new();

            if (productList.Count <= 0)
            {
                _display.PrintMessage("Nothing to display.");
                return;
            }

            foreach (Product product in productList) { productNames.Add(product.ToString()); }

            _inputManager.GetMenuChoice(productNames);

            var choosenProduct = ChooseProduct(productList);
            AddItemToCart(choosenProduct);
        }

        private void DisplayProduct(UnitOfWork unitOfWork, Product product)
        {
            _display.PrintMessage("\n" + product.ToString() +
                "\n" + product.Description + "\n" +
                unitOfWork.Ratings.GetFirstOrDefault(x => x.ProductId.Equals(product.ProductId)) + "\n");
        }

        public void DisplayAllInCart()
        {
            List<string> optionList = new();
            StringBuilder sb = new();

            using var unitOfWork = _unitOfWorkFactory.GetNew();

            var currentUser = unitOfWork.Users.GetFirstOrDefaultWithShoppingCart(u => u.UserId.Equals(_user.UserId));

            if (currentUser.ShoppingCart.ProductList.Count == 0)
            {
                _display.PrintMessage("Your cart is empty.");
                return;
            }

            foreach (var product in currentUser.ShoppingCart.ProductList)
            {
                sb.Clear();
                sb.Append(product);
                sb.Append(", Quantity: ");
                sb.Append(unitOfWork.ShoppingCarts.GetProductQuantity(product, currentUser.ShoppingCart));
                optionList.Add(sb.ToString());
            }
            unitOfWork.CompleteUnit();

            _display.PrintOptions(optionList, "Items in cart");
        }

        public void ManageCart()
        {
            bool isManaging = true;
            while (isManaging)
            {
                using var unitOfWork = _unitOfWorkFactory.GetNew();
                var loggedUser = unitOfWork.
                    Users.
                    GetFirstOrDefaultWithShoppingCart(x => x.UserId == _user.UserId);

                _display.CleanScreen();
                DisplayAllInCart();
                if (loggedUser.ShoppingCart.ProductList.Count == 0)
                {
                    isManaging = false;
                    continue;
                }

                var productId = GetProductIdFromShoppingCart();
                if (productId == -1)
                {
                    isManaging = false;
                    continue;
                }

                switch (_inputManager.FetchStringValue("[R]emove item from cart, [U]pdate item in cart, [S]top adding").ToLower())
                {
                    case "remove" or "r":
                        RemoveItemFromCart(productId);
                        break;
                    case "stop" or "s":
                        isManaging = false;
                        break;
                    case "update" or "u":
                        UpdateItemInCart(productId);
                        break;
                    default:
                        break;
                }
            }
        }

        private Product ChooseProduct(List<Product> listOfProducts)
        {
            Product chosenProduct = new Product();
            var addToCartQuestion = _inputManager.
                FetchStringValue("Do you want to add product to cart? Press [N]o to exit").ToLower();

            if (addToCartQuestion.Equals("n") || addToCartQuestion.Equals("no"))
            {
                return null;
            }

            var isProductInvalid = true;
            while (isProductInvalid)
            {
                int productIdOnTheList = _inputManager.
                    FetchIntValue("Choose product from the list") - 1;
                chosenProduct = listOfProducts.ElementAtOrDefault(productIdOnTheList);
                if (chosenProduct == null) { continue; }

                isProductInvalid = false;
            }

            return chosenProduct;
        }

        private int ChooseProductQuantity(Product chosenProduct)
        {
            int quantityProduct = 0;

            var isQuantityInorrect = true;
            while (isQuantityInorrect)
            {
                quantityProduct = _inputManager.FetchIntValue("Set quantity");

                if (quantityProduct == 0)
                {
                    _display.PrintMessage("You can't set quantity to 0");
                    continue;
                }

                if (quantityProduct > chosenProduct.Amount)
                {
                    _display.PrintMessage("Qunatity of product is not enough");
                    continue;
                }
                isQuantityInorrect = false;
            }

            return quantityProduct;
        }
        private void AddItemToCart(Product productToAdd)
        {
            if (productToAdd == null) { return; }

            using var unitOfWork = _unitOfWorkFactory.GetNew();
            {
                DisplayProduct(unitOfWork, productToAdd);
                var quantity = ChooseProductQuantity(productToAdd);

                var user = unitOfWork.Users.
                    GetFirstOrDefaultWithShoppingCart(x => x.UserId.Equals(_user.UserId));

                if (user.ShoppingCart.ProductList.ToList().Any(x => x.ProductId.Equals(productToAdd.ProductId)))
                {
                    _display.PrintMessage("You already added this product");
                    unitOfWork.CompleteUnit();
                    return;
                }

                user.ShoppingCart.ProductList.Add(productToAdd);
                unitOfWork.CompleteUnit();
                unitOfWork.ShoppingCarts.SetProductQuantity(productToAdd, user.ShoppingCart, quantity);
                unitOfWork.CompleteUnit();
            }
        }
        private void RemoveItemFromCart(int productToRemoveId)
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var customerShoppingCart = unitOfWork.
                Users.
                GetFirstOrDefaultWithShoppingCart(x => x.UserId.Equals(_user.UserId)).
                ShoppingCart;

            int productId = productToRemoveId;
            var itemToRemove = customerShoppingCart.ProductList.ToList()[productId];
            customerShoppingCart.ProductList.Remove(itemToRemove);
            unitOfWork.CompleteUnit();
        }
        private void UpdateItemInCart(int productIdToUpdate)
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var customerShoppingCart = unitOfWork.
                Users.
                GetFirstOrDefaultWithShoppingCart(x => x.UserId.Equals(_user.UserId)).
                ShoppingCart;

            var itemInShoppingCart = customerShoppingCart.
                ProductList.ToList()[productIdToUpdate];

            var itemInDB = unitOfWork.
                Products.
                GetFirstOrDefault(x => x.ProductId.Equals(itemInShoppingCart.ProductId));

            int quantityToChange = 0;
            var isQuantityInorrect = true;
            while (isQuantityInorrect)
            {
                quantityToChange = _inputManager.FetchIntValue("Set new quantity");

                if (quantityToChange == 0)
                {
                    _display.PrintMessage("If u want to remove item from cart, choose proper option");
                    continue;
                }

                if (quantityToChange > itemInDB.Amount)
                {
                    _display.PrintMessage("Qunatity of product is not enough");
                    continue;
                }
                isQuantityInorrect = false;
            }

            unitOfWork.
                ShoppingCarts.
                SetProductQuantity(itemInShoppingCart, customerShoppingCart, quantityToChange);

            unitOfWork.CompleteUnit();
        }

        private void ResetShoppingCart(UnitOfWork unitOfWork, ShoppingCart shoppingCart)
        {
            shoppingCart.ProductList = new List<Product>();
            unitOfWork.ShoppingCarts.Update(shoppingCart);
        }
        private int GetProductIdFromShoppingCart()
        {
            int productId = 0;
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var customerShoppingCart = unitOfWork.
                Users.
                GetFirstOrDefaultWithShoppingCart(x => x.UserId.Equals(_user.UserId)).
                ShoppingCart;

            bool isInputCorrect = true;
            while (isInputCorrect)
            {
                productId = _inputManager.FetchIntValue("Choose product") - 1;
                if (productId == -1) { return productId; }

                if (productId < 0 || productId > customerShoppingCart.ProductList.Count)
                {
                    continue;
                }

                isInputCorrect = false;
            }

            return productId;
        }
        public void SubmitOrder()
        {
            using (var unitOfWork = _unitOfWorkFactory.GetNew())
            {
                var targetUser = unitOfWork.Users.GetFirstOrDefaultWithShoppingCart(x => x.UserId == _user.UserId);
                if (!targetUser.ShoppingCart.ProductList.Any())
                {
                    _display.PrintMessage("Your cart is empty!");
                    return;
                }
                var targetCart = unitOfWork.ShoppingCarts.GetFirstOrDefaultWithProducts(x => x.ShoppingCartId == ((Customer)targetUser).ShoppingCart.ShoppingCartId);
                var productList = targetCart.ProductList;
                if (productList.Count > 0)
                {
                    var newOrder = CreateNewOrder(unitOfWork, targetCart, targetUser);

                    if (!DecreaseProductAmountIfProductStillAvailable(unitOfWork, targetCart)) { return; }

                    unitOfWork.Orders.Add(newOrder);
                    ResetShoppingCart(unitOfWork, targetCart);
                    _display.PrintMessage("You have submitted your order.");
                }
                else
                    _display.PrintMessage("Your cart is empty");

                unitOfWork.CompleteUnit();
            }
        }

        private bool DecreaseProductAmountIfProductStillAvailable(UnitOfWork unitOfWork, ShoppingCart cart)
        {
            foreach (var product in cart.ProductList)
            {
                var amount = unitOfWork.ShoppingCarts.GetProductQuantity(product, cart);
                if (amount > product.Amount)
                {
                    _display.PrintMessage($"Product {product} is no longer available in the requested amount.");
                    return false;
                }
                product.Amount -= amount;
            }
            return true;
        }

        public void DisplayPreviousOrder()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            _display.CleanScreen();
            var orderList = unitOfWork.Orders.GetAllOrdersWhere(x => x.AssignedUser.UserId == _user.UserId).ToList();
            SelectOrderAndDisplayIt(orderList);
            unitOfWork.CompleteUnit();
        }

        private Order CreateNewOrder(UnitOfWork unitOfWork, ShoppingCart shoppingCart, User user)
        {
            var order = new Order();
            order.AssignedUser = user;
            order.CreationDate = DateTime.Now;
            foreach (Product product in shoppingCart.ProductList)
            {
                var newOrderItem = CreateNewOrderItem(product, unitOfWork, shoppingCart);
                order.ItemsList.Add(newOrderItem);
            }
            return order;
        }

        private OrderItem CreateNewOrderItem(Product product, UnitOfWork unitOfWork, ShoppingCart shoppingCart)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.ProductName = product.ProductName;
            orderItem.Price = product.GetCurrentPrice();
            orderItem.Quantity = unitOfWork.ShoppingCarts.GetProductQuantity(product, shoppingCart);
            orderItem.ProductId = product.ProductId;
            return orderItem;
        }
        private OrderItem SelectOrderItem(Order order)
        {
            var orderItemList = order.ItemsList.ToList();
            var targetOrderItem = _inputManager.GetItemFromList(orderItemList);
            return targetOrderItem;
        }

        private void SelectOrderAndDisplayIt(List<Order> orderList)
        {
            if (orderList.Count > 0)
            {
                var targetOrder = _inputManager.GetItemFromList(orderList);
                var productList = targetOrder.ItemsList;
                _view.DisplayAll(productList.ToList());
            }
            else _display.PrintMessage("There are no orders in your history");
        }

        public void RateProduct()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            _display.CleanScreen();
            var orderList = unitOfWork.Orders.GetAllOrdersWhere
                (x => x.AssignedUser.UserId == _user.UserId && x.OrderStatus == OrderStatus.Delivered).ToList();

            if (!orderList.Any())
            {
                _display.PrintMessage("There are no orders to rate");
                return;
            }
            var targetOrder = _inputManager.GetItemFromList(orderList);
            var targetOrderItem = SelectOrderItem(targetOrder);
            RateSelectedProduct(targetOrderItem, unitOfWork);

            unitOfWork.CompleteUnit();
        }
        private void RateSelectedProduct(OrderItem orderItem, UnitOfWork unitOfWork)
        {
            var targetId = orderItem.ProductId;
            var TargetRating = unitOfWork.Ratings.GetFirstOrDefaultWithCustomers(x => x.ProductId == targetId);

            if (TargetRating != null)
                UpdateRating(TargetRating, unitOfWork);
            else
                CreateNewRating(targetId, unitOfWork);

            unitOfWork.CompleteUnit();
        }

        private void CreateNewRating(int productId, UnitOfWork unitOfWork)
        {
            var ratingScore = _inputManager.GetNumberInBetween(1, 5, "How many stars would you like to give? 1 - 5");
            var rating = new Rating();
            rating.CurrentRating = ratingScore;
            rating.NumberOfRatings = 1;
            rating.ProductId = productId;
            var targetUser = unitOfWork.Users.GetFirstOrDefaultWithRatings(x => x.UserId == _user.UserId);
            targetUser.Ratings.Add(rating);
            unitOfWork.CompleteUnit();
        }

        private void UpdateRating(Rating rating, UnitOfWork unitOfWork)
        {
            if (unitOfWork.Ratings.IsRatingAlreadyExist(x => x.ProductId == rating.ProductId && x.Customers.Any(x => x.UserId == _user.UserId)))
                _display.PrintMessage("You have already rated this product");
            else
            {
                var ratingScore = _inputManager.GetNumberInBetween(1, 5, "How many stars would you like to give? 1 - 5");
                rating.CurrentRating = (rating.CurrentRating * rating.NumberOfRatings + ratingScore) / (rating.NumberOfRatings + 1);
                rating.NumberOfRatings = rating.NumberOfRatings + 1;
                var currentCustomer = unitOfWork.Users.GetFirstOrDefaultWithRatings(x => x.UserId == _user.UserId);
                rating.Customers.Add(currentCustomer);
                unitOfWork.Ratings.Update(rating);
            }
        }
        public void ShowStats()
        {
            using var unitOfWork = _unitOfWorkFactory.GetNew();
            var allOrders = unitOfWork.Orders.GetAllOrdersWhere(x => x.AssignedUser.UserId == _user.UserId).ToList();
            int numberOfOrders = allOrders.Count;
            int numberOfProducts = countAllProductsInOrders(allOrders);
            decimal moneySpent = countAllMoneySpentOnOrders(allOrders);
            _display.PrintMessage("Your stats:");
            _display.PrintMessage($"Number of orders submited: {numberOfOrders}\nNumber of products ordered: {numberOfProducts}\nMoney spent: {moneySpent}");
        }

        private int countAllProductsInOrders(List<Order> orders)
        {
            int result = 0;
            foreach (var order in orders)
                result = result + countAllProductsInTargetOrder(order);

            return result;
        }

        private int countAllProductsInTargetOrder(Order order)
        {
            int result = 0;
            foreach (OrderItem orderItem in order.ItemsList)
                result = result + orderItem.Quantity;

            return result;
        }

        private decimal countAllMoneySpentOnOrders(List<Order> orders)
        {
            decimal result = 0;
            foreach (Order order in orders)
                result = result + countMoneySpentOnTargetOrder(order);

            return result;
        }

        private decimal countMoneySpentOnTargetOrder(Order order)
        {
            decimal result = 0;
            foreach (OrderItem orderItem in order.ItemsList)
                result = result + orderItem.Price * orderItem.Quantity;

            return result;
        }
    }
}
