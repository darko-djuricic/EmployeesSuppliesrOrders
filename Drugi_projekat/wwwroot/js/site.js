// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const ls_variable = "saved_products"
let base64String = "";

function imageUploaded() {
    var file = document.querySelector(
        'input[type=file]')['files'][0];

    var reader = new FileReader();

    reader.onload = function () {
        base64String = reader.result.replace("data:", "")
            .replace(/^.+,/, "");

        imageBase64Stringsep = base64String;
        document.getElementById("profile_pic").setAttribute("src", `data: image / png; base64, ${base64String}`)
        document.getElementById("Picture").value = base64String;
        
    }
    reader.readAsDataURL(file);
}


document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('show-nav')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

});

function AddProduct() {
    let product = $('#addProductForm').serializeArray().reduce(function (json, { name, value }) {
                    json[name] = value;
                    return json;
    }, {});
    product.UnitPrice = parseFloat(product.UnitPrice)
    product.Discontinued = $("#Discontinued").prop('checked')

    if (!Validation(product)) return;

    product.Supplier = $("#SupplierId option:selected").text()
    product.Category = $("#CategoryId option:selected").text()
    product.Id = UniqueID();
    let text = `Supplier: ${product.Supplier}, Category: ${product.Category},<br> Quantity per unit: ${product["QuantityPerUnit"]}, Unit price: ${product["UnitPrice"]},<br> Units in stock: ${product["UnitsInStock"]}, Units on order: ${product["UnitsOnOrder"]},<br> Reordered level: ${product["ReorderedLevel"]}, Discontinued: ${product["Discontinued"] ? "Yes" : "No"}`
    let template = `<div class="col-auto" data-product-id='${product.Id}'><a class="btn btn-light border col-auto me-2" data-bs-toggle="popover" data-bs-html="true" data-bs-placement="top" title="${product["ProductName"]}" data-bs-content="${text}">
                <span>${product["ProductName"]} <span class="btn-close ms-3" data-product-id="${product.Id}" onclick="Remove('${product.Id}')"></span></span>
                </a>
                <input type="hidden" name="SupplierId" value="${product["SupplierId"]}" />
                <input type="hidden" name="CategoryId" value="${product["CategoryId"]}" />
                <input type="hidden" name="ProductName" value="${product["ProductName"]}" />
                <input type="hidden" name="QuantityPerUnit" value="${product["QuantityPerUnit"]}" />
                <input type="hidden" name="UnitPrice" value="${product["UnitPrice"]}" />
                <input type="hidden" name="UnitsInStock" value="${product["UnitsInStock"]}" />
                <input type="hidden" name="UnitsOnOrder" value="${product["UnitsOnOrder"]}" />
                <input type="hidden" name="ReorderLevel" value="${product["ReorderLevel"]}" />
                <input type="hidden" name="Discontinued" value="${product["Discontinued"]}" />
                </div>`;
    $("#products").append(template)

    let saved = JSON.parse(localStorage.getItem(ls_variable)) ?? [];
    saved.push(product);
    localStorage.setItem(ls_variable, JSON.stringify(saved))

    $("#addProductForm").trigger("reset");
    SetPopover();
}

function SavedProducts() {
    let saved = JSON.parse(localStorage.getItem(ls_variable)) ?? [];

    saved.forEach(p => {
        
        let text = `Supplier: ${p["Supplier"]}, Category: ${p["Category"]},<br> Quantity per unit: ${p["QuantityPerUnit"]}, Unit price: ${p["UnitPrice"]},<br> Units in stock: ${p["UnitsInStock"]}, Units on order: ${p["UnitsOnOrder"]},<br> Reordered level: ${p["ReorderedLevel"]}, Discontinued: ${p["Discontinued"] ? "Yes" : "No"}`
        let template = `<div data-product-id="${p.Id}" class="col-auto"><a class="btn btn-light border" data-bs-toggle="popover" data-bs-html="true" data-bs-placement="top" title="${p["ProductName"]}" data-bs-content="${text}">
                <span>${p["ProductName"]} <span class="btn-close ms-3" onclick="Remove('${p.Id}')"></span></span>
            </a>
            <input type="hidden" name="SupplierId" value="${p["SupplierId"]}" />
            <input type="hidden" name="CategoryId" value="${p["CategoryId"]}" />
            <input type="hidden" name="ProductName" value="${p["ProductName"]}" />
            <input type="hidden" name="QuantityPerUnit" value="${p["QuantityPerUnit"]}" />
            <input type="hidden" name="UnitPrice" value="${p["UnitPrice"]}" />
            <input type="hidden" name="UnitsInStock" value="${p["UnitsInStock"]}" />
            <input type="hidden" name="UnitsOnOrder" value="${p["UnitsOnOrder"]}" />
            <input type="hidden" name="ReorderLevel" value="${p["ReorderLevel"]}" />
            <input type="hidden" name="Discontinued" value="${p["Discontinued"]}" />
            </div>`;
        $("#products").append(template)
    })

}

function Validation(product) {
    let valid = true;
    if (product["SupplierId"]) {
        $("#SupplierId").removeClass("input-validation-error")
        $("#SupplierId").addClass("valid");
        $("#SupplierId").attr("aria-invalid", "false")
        $("#SupplierId").parent().find("span").addClass("field-validation-valid")
        $("#SupplierId").parent().find("span").removeClass("field-validation-error")
    }
    else {
        $("#SupplierId").addClass("input-validation-error")
        $("#SupplierId").removeClass("valid")
        $("#SupplierId").attr("aria-invalid", "true")
        $("#SupplierId").parent().find("span").removeClass("field-validation-valid")
        $("#SupplierId").parent().find("span").addClass("field-validation-error")
        valid = false;
    }

    if (product["ProductName"]) {
        $("#ProductName").removeClass("input-validation-error")
        $("#ProductName").addClass("valid");
        $("#ProductName").attr("aria-invalid", "false")
        $("#ProductName").parent().find("span").addClass("field-validation-valid")
        $("#ProductName").parent().find("span").removeClass("field-validation-error")
    }
    else {
        $("#ProductName").addClass("input-validation-error")
        $("#ProductName").removeClass("valid")
        $("#ProductName").attr("aria-invalid", "true")
        $("#ProductName").parent().find("span").removeClass("field-validation-valid")
        $("#ProductName").parent().find("span").addClass("field-validation-error")
        valid = false;
    }

    if (product["UnitPrice"]) {
        $("#UnitPrice").removeClass("input-validation-error")
        $("#UnitPrice").addClass("valid");
        $("#UnitPrice").attr("aria-invalid", "false")
        $("#UnitPrice").parent().find("span").addClass("field-validation-valid")
        $("#UnitPrice").parent().find("span").removeClass("field-validation-error")
    }
    else {
        $("#UnitPrice").addClass("input-validation-error")
        $("#UnitPrice").removeClass("valid")
        $("#UnitPrice").attr("aria-invalid", "true")
        $("#UnitPrice").parent().find("span").removeClass("field-validation-valid")
        $("#UnitPrice").parent().find("span").addClass("field-validation-error")
        valid = false;
    }
    return valid;
}

function SetPopover() {
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    })
}

function Remove(id) {
    $("#products").find(`[data-product-id='${id}']`).remove();
    var products = JSON.parse(localStorage.getItem(ls_variable))
    products = products.filter(p => p.Id != id)
    localStorage.setItem(ls_variable, JSON.stringify(products));
}

function UniqueID() {
    return Date.now().toString(36) + Math.random().toString(36).substr(2);
}

$('#save_form').on('submit', function () {
    localStorage.removeItem(ls_variable);
});

SavedProducts();
SetPopover();