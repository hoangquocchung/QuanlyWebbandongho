using DataBaseIO.DBIO;
using DoAnShopDongHo.Models;
using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoAnShopDongHo.Controllers
{
    public class CartController : Controller
    {
        private const string CartSesion = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSesion];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSesion];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSesion] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                // gán vào session
                Session[CartSesion] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSesion];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsoncart.SingleOrDefault(x => x.product.ID == item.product.ID);
                if (jsoncart != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSesion] = sessionCart;
            return Json(new
            {
                status = true,
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSesion]; // lấy ra danh sách giỏ hàng
            sessionCart.RemoveAll(x => x.product.ID == id); // remove những id được truyền vào
            Session[CartSesion] = sessionCart;
            return Json(new
            {
                status = true,
            });
        }

        public ActionResult Payment()
        {
            var cart = Session[CartSesion];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);

        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSesion];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.product.ID;
                    orderDetail.OrderID = id;
                    
                    if (item.product.PromotionPrice == null)
                    {
                        total = (item.product.Price.GetValueOrDefault(0) * item.Quantity);
                        orderDetail.Price = total;
                    }
                    else
                    {
                        total = (item.product.PromotionPrice.GetValueOrDefault(0) * item.Quantity);
                        orderDetail.Price = total;
                    }
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                }
            }
            catch
            {
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}