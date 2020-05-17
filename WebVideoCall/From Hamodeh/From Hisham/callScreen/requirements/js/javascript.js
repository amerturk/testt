   $(document).ready(function () {

     
    $('.SlideGuidGif').delay(6000).fadeOut()   
       

   	$(".MainSlider").slick({
   		infinite: true,
   		slidesToShow: 1,
   		dots: true,
   		prevArrow: false,
   		nextArrow: false,
   		slidesToScroll: 1,
   		autoplay: true,
   		autoplaySpeed: 2000,

   	});



   	$(".slick-dots button").each(function () {
   		$(this).html('<i class="fas fa-circle"></i>')
   	});




   	if ((screen.width < 1400) || (window.innerWidth < 1400)) {
   		$(".CouponSlider").slick({
   			draggable: true,
   			autoplay: true,
   			autoplaySpeed: 1500,
   			infinite: true,
   			slidesToShow: 3,
   			slidesToScroll: 1,
   			touchThreshold: 1000,
   			arrows: false,
   			dots: false,
   			mobileFirst: true,
   			swipeToSlide: true,
   		});
   		$(".DiscountSlider").slick({
   			draggable: true,
   			autoplay: false,
   			infinite: true,
   			slidesToShow: 2,
   			slidesToScroll: 1,
   			touchThreshold: 1000,
   			arrows: true,
   			dots: false,
   			prevArrow: $('.prevDiscount'),
   			nextArrow: $('.nextDiscount'),
   			swipeToSlide: true,
   		});

   		$('.onResponsive').each(function () {
   			$(this).removeClass('col-md');
   			$(this).addClass('col-md-6');
   		});

   		$('.DiscountSliderButtons').hide();
		
		
   		$('.fa-eye').click(function () {
   			$('.passwordText').prop('type', 'text')
   		});

   	} else {
   		$(".CouponSlider").slick({
   			draggable: true,
   			autoplay: true,
   			autoplaySpeed: 1500,
   			infinite: true,
   			slidesToShow: 5,
   			slidesToScroll: 3,
   			touchThreshold: 1000,
   			arrows: false,
   			dots: false,
   			mobileFirst: true,
   		});
   		$(".DiscountSlider").slick({
   			draggable: true,
   			autoplay: false,
   			infinite: true,
   			slidesToShow: 3,
   			slidesToScroll: 1,
   			touchThreshold: 1000,
   			arrows: true,
   			dots: false,
   			prevArrow: $('.prevDiscount'),
   			nextArrow: $('.nextDiscount'),
   		});

   	}






   });









   $(".Coupon-item").hover(function () {
   	$(this).find(".addBasketBtn").slideToggle();
   	$(this).find(".CompanyImg").toggleClass("CompanyImgOnHover")
   });



   AOS.init();


   $(".Categorie-item").hover(function () {
   	$(this).find("i").toggleClass('categoriesOnHover')
   });



   $('.LoginPoPuP').click(function () {
   	//  $('.LoginPoPuP').show()
   });



   $('.fa-eye').mousedown(function () {
   	$('.passwordText').prop('type', 'text');

   });
   $('.fa-eye').mouseup(function () {
   	$('.passwordText').prop('type', 'password');

   });


   $('.loginSub').click(function () {
   	$('.LoginDiv').hide()
   	$('.LoginFade').hide()
   	$('.LoginPoPuP').hide()
   	$('.UserNameDiv').show()
   	$('.UserNameDiv').find('p').html($('.UserNameInput').val())
   });



   $('.DiscountItem').hover(function () {
   	$(this).find('h1').removeClass('DiscountItem')
   	$(this).find('h1').toggleClass('percantageOnHover')
   	$(this).find('h1').hover()
   	$(this).find('img').toggleClass('DiscountItemOnHover')
   });


$('.characterMain').hover(function(){
   $('.Light').toggle() 
});
