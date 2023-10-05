$(document).ready(() => {
    $('.add-to-cart').on("click", AddToCart);
    $(".btn-delete").on("click", function () {
        var productid = $(this).data('productid');       
        $.ajax({
            data: { productid: productid },
            url: "/Giohang/CartDel",
            //dataType: "json",
            type: 'POST',
            success: function (res) {                 
                if (res.status == true) {
                    window.location.href = '/gio-hang';
                }
            }
        });
    });
});

function AddToCart() {
    var productid = $(this).data('productid');
    var quantity = $('#sst') ? $('#sst').val() : 1;
    $.ajax({
        data: { productid, quantity },
        url: "/Giohang/CartAdd",      
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                $('#so-luong').html(res.qty);
                Swal.fire({
                   // position: 'top-end',
                    icon: 'success',
                    title: 'thêm sản phẩm thành công',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        }
    });
}
function UpdateCart(productid, qty) {    
    if (qty == 0) {
        RemoveCartItem(productid);
    }
    else {
        UpdateCartItem(productid, qty);
    }
}
function RemoveCartItem(productid) {
    $.ajax({
        data: { productid },
        url: "/Giohang/CartDel",
        type: 'POST',
        success: function (res) {
            $('#total-price').html(res.totalPrice);
            if (res.status == true) {
                window.location.href = '/gio-hang';
            }            
        }
    });
}

const VND = new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND',
});
function UpdateCartItem(productid, qty) {
   // alert(qty);
    $.ajax({
        data: { productid, qty },
        url: "/Giohang/CartUpdate",
        type: 'POST',
        success: function (res) {             
            $.each(res.listcart, function (i, value) {
                if (value.productId == productid) {
                    $(`#${productid}`).html(value.Amuont.toLocaleString({ style: "currency", currency: "VND" }).replace(/,/g, ".") + " đồng");
                }               
            })           
            $('#total-price').html(res.totalPrice + " đồng");         
        }
    });
}

function clearCart() {
    if (confirm('Bạn muốn xóa toàn bộ sản phẩm trong giỏ hàng?')) {
        $.ajax({
            url: "/Giohang/CartClearAll",
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    window.location.href = '/gio-hang';
                }
            }
        });
    }
}