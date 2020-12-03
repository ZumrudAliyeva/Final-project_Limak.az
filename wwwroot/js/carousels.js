'use script'
$(function () {

    // HOME //
    /*== Home main carousel ==*/
    $('.slider-main').owlCarousel({
        loop: true,
        autoplay: true,
        margin: 0,
        nav: false,
        dots: true,
        responsiveClass: true,
        responsive: {
            1200: {
                items: 1,
                stagePadding: 0,
            },
            768: {
                items: 1,
                stagePadding: 0,
            },
            375: {
                items: 1,
                stagePadding: 0,
            },
        }
    })
    /*== Home blogs carousel ==*/

    $('.logos').owlCarousel({
        loop: true,
        autoplay: true,
        margin: 50,
        nav: false,
        dots: false,
        responsiveClass: true,
        responsive: {
            1200: {
                items: 5,
                stagePadding: 0,
            },
            768: {
                items: 3,
                stagePadding: 0,
            },
            375: {
                items: 1,
                stagePadding: 0,
            },
        }
    })

    // $('.list-collection').owlCarousel({
    //     loop: false,
    //     autoplay: false,
    //     nav: false,
    //     dots: false,
    //     responsiveClass: true,
    //     responsive: {
    //         1200: {
    //             items: 5,
    //         },
    //         768: {
    //             items: 5,
    //         },
    //         375: {
    //             items: 3,
    //         },
    //     }
    // })

    // $('.reason-list').owlCarousel({
    //     loop: true,
    //     autoplay: true,
    //     nav: false,
    //     dots: false,
    //     responsiveClass: true,
    //     responsive: {
    //         1200: {
    //             items: 3,
    //             loop: false,
    //             autoplay: false,
    //         },
    //         768: {
    //             items: 2,
    //             loop: true,
    //             autoplay: true,
    //         },
    //         375: {
    //             items: 1,
    //             loop: true,
    //             autoplay: true,
    //         },
    //     }
    // })

    /*== About us Instagram carousel ==*/
    // $('.instagram-galery').owlCarousel({
    //     loop:true,
    //     autoplay:false,
    //     nav: false,
    //     dots: false,
    //     responsiveClass:true,
    //     responsive:{
    //         1200:{
    //             items: 4,
    //             stagePadding: 0,
    //         },
    //         768:{
    //             items: 4,
    //             stagePadding: 0,
    //         },
    //         375:{
    //             items: 2,
    //             stagePadding: 0,
    //         },
    //     }
    // })


})