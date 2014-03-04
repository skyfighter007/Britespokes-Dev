$(document).ready(function () {
  $('ul.nav li.sub').hover(
    function () {
      $(this).find('ul').slideDown();
    },
    function () {
      $(this).find('ul').slideUp();
    }
  );

 
});