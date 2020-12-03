"use strict"

$(function () {
    
    $('a[href=""]').click(function (event) {
        event.preventDefault();
    });
    //$('button').click(function (event) {
    //    event.preventDefault();
    //});

    /*============= register =============== */
    $(document).on('click', 'button#gender', function () {
        $('ul.list_gender').toggleClass('active');
    })
    $(document).on('click', 'ul.list_gender li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="gender"] option`).attr('selected', false);
        let selectedoption = $(`select[name="gender"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.list_gender').toggleClass('active');
        $('button#gender').text(selectedoption.text());
        console.log(selectedoption);
    })

    $(document).on('click', 'button#day', function () {
        $('ul.list_day').toggleClass('active');
    })
    $(document).on('click', 'ul.list_day li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="day"] option`).attr('selected', false);
        let selectedoption = $(`select[name="day"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.list_day').toggleClass('active');
        $('button#day').text(selectedoption.text());
        console.log(selectedoption);
    })

    $(document).on('click', 'button#month', function () {
        $('ul.list_month').toggleClass('active');
    })
    $(document).on('click', 'ul.list_month li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="month"] option`).attr('selected', false);
        let selectedoption = $(`select[name="month"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.list_month').toggleClass('active');
        $('button#month').text(selectedoption.text());
        console.log(selectedoption);
    })

    $(document).on('click', 'button#year', function () {
        $('ul.list_year').toggleClass('active');
    })
    $(document).on('click', 'ul.list_year li', function () {
        let selectedVal = $(this).find('a').attr('data-value');
        $(`select[name="year"] option`).attr('selected', false);
        let selectedoption = $(`select[name="year"] option[value="${selectedVal}"]`).attr('selected', true);
        $('ul.list_year').toggleClass('active');
        $('button#year').text(selectedoption.text());
        console.log(selectedoption);
    })

    $(document).on('focus', 'input.inputText', function () {
        $(this).addClass('active');
        $(this).parent().find('.input_span').addClass('active');
    });

    $(document).on('blur', 'input.inputText', function () {
        $(this).removeClass('active');
    })


    $(document).on('click', '.checkmark', function () {
        $('.checkmark').toggleClass('active');
    })

    //============ login ===============

    $(document).on('click', '.login_link', function () {
        $('.login-menu-bg').addClass('active');
    })

    $(document).on('click', 'button.close', function () {
        $('.login-menu-bg').removeClass('active');
    })



    
})