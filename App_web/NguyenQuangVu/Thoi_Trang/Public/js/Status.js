$(document).ready(() => {
    $('.btn-status').off("click").on("click", changeStatus);
});
function changeStatus() {
    var btn = $(this)
    var id = btn.data('id');
    $.ajax({
        data: { id: id },
        url: "/Admin/Order/Status",
        type: 'POST',
        success: function (res) {
            console.log(res)
            if (res.status == 1) {
                btn.css({
                    "color": "#fff",
                    "background-color": "#28a745",
                    "border-color": "#28a745",
                    "box-shadow": "none"
                });
                btn.html('<i class="fas fa-toggle-on" data-toggle="tooltip" data-placement="right" title="Đã duyệt"></i>');
            }
            else {
                btn.css({
                    "color": "#fff",
                    "background-color": "#dc3545",
                    "border-color": "#dc3545",
                    "box-shadow": "none"
                });
                btn.html('<i class="fas fa-toggle-off" data-toggle="tooltip" data-placement="right" title="Chờ duyệt"></i>');
            }
        }
    });
}

function UpdateItem(orderId, productId, qty) {
    if (qty == 0) {
        RemoveItem(orderId, productId);
    }
    else {
        UpdateProductItem(orderId, productId, qty);
    }
}
function UpdateProductItem(orderId, productId, qty) {
    var tongtien = document.getElementById("tongtien");
    var tien = 0;
    $.ajax({
        data: { orderId, productId, qty },
        url: "/Admin/Order/UpdateOrderdetail",
        type: 'POST',
        success: function (res) {
            $.each(res.listProduct, function (i, value) {
                if (value.ProductId == productId) {
                    $(`#${productId}`).html(value.Amount.toLocaleString({ style: "currency", currency: "VND" }).replace(/,/g, ".") + " VND");
                    $(`#Id-${productId}`).html(value.Amount);
                }
            });
            $('#tong-tien').html(res.totalPrice.toLocaleString({ style: "currency", currency: "VND" }).replace(/,/g, ".") + " VND");
            tongtien.value = res.totalPrice;
            $('#tongtien').html(tongtien.value);
        }
    });
}

function RemoveItem(orderId, productId) {
    $.ajax({
        data: { orderId, productId },
        url: "/Admin/Order/DeleteOrderdetail",
        type: 'POST',
        success: function (res) {         
            if (res.status == true) {
                $('#tong-tien').html(res.totalPrice + "VND");
                $('#tongtien').html(res.totalPrice);
                if (res.soluongSp > 0) {
                    window.location.href = `/Admin/Order/Edit/${orderId}`;
                }
                else {
                    window.location.href = '/Admin/Order/Index';
                }
            }
        }
    });
}
function DeleteItem(orderId, productId) {
    let check = confirm('Bạn muốn xóa sản phẩm này?');
    if (check == true) {
        $.ajax({
            data: { orderId, productId },
            url: "/Admin/Order/DeleteOrderdetail",
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    $('#tong-tien').html(res.totalPrice + "VND");
                    $('#tongtien').html(res.totalPrice);
                    if (res.soluongSp > 0) {
                        window.location.href = `/Admin/Order/Edit/${orderId}`;
                    }
                    if (res.soluongSp == 0) {
                        window.location.href = '/Admin/Order/Index';
                    }
                }
            }
        });
    }
}