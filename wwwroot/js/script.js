"use strict"

$(function () {
    
    $('a[href=""]').click(function (event) {
        event.preventDefault();
    });
    $('button').click(function (event) {
        event.preventDefault();
    });

/*============= register =============== */
//$(document).on('click', 'button#gender', function () {
//    $('ul.list_gender').toggleClass('active');
//})
//$(document).on('click', 'ul.list_gender li', function () {
//    let selectedVal = $(this).find('a').attr('data-value');
//    $(`select[name="gender"] option`).attr('selected', false);
//    let selectedoption = $(`select[name="gender"] option[value="${selectedVal}"]`).attr('selected', true);
//    $('ul.list_gender').toggleClass('active');
//    $('button#gender').text(selectedoption.text());
//    console.log(selectedoption);
//})

$(document).on('focus', 'input.inputText', function () {
    $(this).addClass('active');
    $('span.input_span').removeClass('active');
    $(this).next().addClass('active');
});

$(document).on('blur', 'input.inputText', function () {
    $(this).removeClass('active');
    $(this).next().removeClass('active');
})


$(document).on('click', '.checkmark', function () {
    $('.checkmark').removeClass('active');
    $(this).toggleClass('active');
})
$(document).on('click', '.checkmark-radio', function () {
    $('.checkmark-radio').removeClass('active');
    $(this).toggleClass('active');
})


    $(document).on('click', '.ID_btn', function () {
        $('.modal_fin').addClass('active');
    })

    $(document).on('click', '.close_btn', function () {
        $('.modal_fin').removeClass('active');
    })

    $(document).on('click', '.modal_fin', function () {
        $('.modal_fin').removeClass('active');
    })
//============ login ===============

$(document).on('click', '.login_link', function () {
    $('.login-menu-bg').addClass('active');
    LetLogin();
})

$(document).on('click', 'button.close', function () {
    $('.login-menu-bg').removeClass('active');
})

    function LetLogin() {
        var data = $("#loginForm").serialize();
        $.ajax({
            type: "post",
            url: "/Account/Login",
            data: data,
            success: function (data) {
                $('.login-menu-bg').empty().append(data);
            }

            })
    }

// Order

$(document).on('change', '#cargo_place', function () {
    let selectedPl = $(this).find('option:selected').val();
    if (selectedPl == "yes") {
        $('div.cargo_price').addClass('active');
    }
    else
    {
        $('div.cargo_price').removeClass('active');
    }
    console.log(selectedPl);
})

// User panel

$(document).on('click', 'ul.userPanelList li', function () {
    $('ul.userPanelList li').removeClass('active');
    $(this).addClass('active');
})

// Calculate PriceResult

    $(document).on('change', 'input.inlandornot', function () {
        var inlandornot = $("input.inlandornot").is(":checked").valueOf();
        if (inlandornot == true) {
            $('div.cargo_price').addClass('active');
        } else {
            $('div.cargo_price').removeClass('active');
        }
    })


    $(document).on('change', '.prInput', function () {
        var value = $(this).val();
        Calculate_prResult(value);
    });

    function Calculate_prResult (){
        var totalPrice =
            parseInt($('.quantityInput').val()) *
            (parseInt($('.priceInpt').val()) +
            parseInt($('.priceInpt').val()) * 0.05 +
            parseInt($('.cargoInpt').val()) +
            parseInt($('.cargoInpt').val()) * 0.05)

        $('.priceResult').val(totalPrice);
        console.log(totalPrice);
    }


    $(document).on('click', '.admin-li', function () {
        $('.admin_menu').toggleClass('d-none');
    });

    $(document).on("click", ".userPanelList li", function () {
        var index = $(this).attr('data-index');
        var currentContent = $(`.panel_tab_content .tabContent[data-index="${index}"]`);
        $('.panel_tab_content .tabContent').addClass('d-none');
        $('.panel_tab_content .tabContent').removeClass('d-block');
        $(currentContent).removeClass('d-none');
        $(currentContent).addClass('d-block');
    });


    $(document).on("click", ".userPanelList li[data-index='orders']", function () {
        statusId = 0;
        GetOrders();
    });
    $(document).on("click", ".userPanelList li[data-index='declarations']", function () {
        declarationStatusId = 0;
        GetDeclarations();
    });

    $(document).on("click", ".user_menu li[data-index='orders']", function () {
        statusId = 0;
        GetOrders();
    });
    $(document).on("click", ".user_menu li[data-index='declarations']", function () {
        declarationStatusId = 0;
        GetDeclarations();
    });


    var statusId = 0;
    var declarationStatusId = 0;


    GetOrders();
    GetDeclarations();


    $(document).on("click", ".orders li", function () {
        orderStatusId = $(this).find("a").attr("href").split('/')[2];
        $(".orders li").removeClass("active-li");
        $(this).addClass("active-li");
        GetOrders();
    });


    $(document).on("click", ".declarations li", function () {
        declarationStatusId = $(this).find("a").attr("href").split('/')[2];
        $(".declarations li").removeClass("active-li");
        $(this).addClass("active-li");
        GetDeclarations();
    });


    function GetOrders() {
        $.ajax({
            url: "GetAllOrdersWithStatusId",
            type: "Post",
            dataType: "html",
            data: { id: statusId },
            success: function (data) {
                $('.myordersList').empty().append(data);
            }
        })
    }

    function GetDeclarations() {
        $.ajax({
            url: "GetAllDeclarationsWithStatusId",
            type: "Post",
            dataType: "html",
            data: { id: declarationStatusId },
            success: function (data) {
                $('.mydeclarationsList').empty().append(data);
            }
        })
    }

    $(document).on('click', '.user-li', function () {
        $('.user_menu').toggleClass('d-none');
    });




    //KALKULYATOR

    $(document).on('click', 'button.calc_place, button.calc-btn', function () {
        //$('.dropdown_menu_calc').removeClass('active');
        $(this).next().next().toggleClass('active');
    });


    $(document).on('click', 'ul.country li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="country"] option`).attr('selected', false);
        let selectedCountry = $(`select[name="country"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.country').toggleClass('active');
        $('button#country').text(selectedCountry.text());
        console.log(selectedCountry);
    })

    $(document).on('click', 'ul.weight li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="weight"] option`).attr('selected', false);
        let selectedWeight = $(`select[name="weight"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.weight').toggleClass('active');
        $('button#weight-btn').text(selectedWeight.text());
        console.log(selectedWeight);

    })

    $(document).on('click', '#calculate', function () {
        Calculate_amount();
    })

    function Calculate_amount() {
        var sum = 0.00;

        var country = $('select[name=country]').find('option:selected').val();
        var weight = 0;
        var weight_type = $("select[name=weight]").find('option:selected').val();
        var quantity = $('input#count_pack').val();

        if (weight_type == "kg") {
            weight = $("input#weight").val();
        }
        else {
            weight = $("input#weight").val() / 1000;
        }

        if (country == "turkiye") {
            if (weight > 0 && weight <= 0.25) {
                sum = 2.00 * quantity;
            }
            if (weight > 0.25 && weight <= 0.5) {
                sum = 3.00 * quantity;
            }
            if (weight > 0.5 && weight <= 0.7) {
                sum = 4.00 * quantity;
            }
            if (weight > 0.7 && weight <= 1) {
                sum = 4.50 * quantity;
            }
            if (weight > 1) {
                sum = weight * 4.50 * quantity;
            }
        }

        if (country == "amerika") {
            if (weight > 0 && weight <= 0.25) {
                sum = 1.99 * quantity;
                }
                if (weight > 0.25 && weight <= 0.5) {
                    sum = 3.99 * quantity;
                }
                if (weight > 0.5 && weight <= 0.7) {
                    sum = 4.99 * quantity;
                }
                if (weight > 0.7 && weight <= 1) {
                    sum = 5.99 * quantity;
                }
            if (weight > 1) {
                sum = weight * 5.99 * quantity
                }
        }

        if (country == "amerika") {
            $("#priceResult").text("$ " + sum);
        }
        else
        {
            $("#priceResult").text("tl " + sum);
        }
        console.log(sum);
    }


////////////////////////////////////////////////////////////////////////

    //$(document).on('click', 'div.packageList', function () {
    //    $('div.packageList ul').toggleClass('d-none');
    //});


})