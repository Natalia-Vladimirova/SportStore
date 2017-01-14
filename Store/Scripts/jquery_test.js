 $(document).ready(function(){
	
     
var sub = true;
$('#buttonsubmitreg').attr('disabled');
     $('#UserName, #Email, #Password,#ConfirmPassword').unbind().blur( function(){

        // ƒл€ удобства записываем обращени€ к атрибуту и значению каждого пол€ в переменные
         var id = $(this).attr('id');
         var val = $(this).val();

       // ѕосле того, как поле потер€ло фокус, перебираем значени€ id, совпадающие с id данного пол€
       switch(id)
       {
             // ѕроверка пол€ "»м€"
             case 'UserName':
                var rv_name = /^[a-zA-Zа-€ј-я]+$/; // используем регул€рное выражение

                // Eсли длина имени больше 2 символов, оно не пустое и удовлетвор€ет рег. выражению,
                // то добавл€ем этому полю класс .not_error,
                // и ниже в контейнер дл€ ошибок выводим слово " ѕрин€то", т.е. валидаци€ дл€ этого пол€ пройдена успешно

                if(val.length > 1 && val != '' && rv_name.test(val))
                {
                    $(this).css('background', '#A4FF83');

                }

              // »наче, мы удал€ем класс not-error и замен€ем его на класс error, говор€ о том что поле содержит ошибку валидации,
              // и ниже в наш контейнер выводим сообщение об ошибке и параметры дл€ верной валидации

                else
                {
                   alert("Error in filed Name!");
                   $(this).css('background', '#FF3C1D');
                   sub = false;
                   if (sub == false) {
                       sub = true;
                       $('#UserName').val('');
                       $('#UserName').css('background', '#fff');
                   } else {
                       $('#buttonsubmitreg').removeAttr('disabled');
                   }
                }
            break;

           // ѕроверка email
           case 'Email':
               var rv_email = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
               if(val != '' && rv_email.test(val))
               {
                   $(this).css('background', '#A4FF83');

               }
               else
               {
                  alert("Error in filed Email!");
                  $(this).css('background', '#FF3C1D');
                  sub = false;
                  if (sub == false) {
                      sub = true;
                      $('#Email').val('');
                      $('#Email').css('background', '#fff');
                  } else {
                      $('#buttonsubmitreg').removeAttr('disabled');
                  }
               }
           break;


          // ѕроверка пол€ "—ообщение"
          case 'Password':
              var rv_email = /^([a-zA-Z0-9_.-])/;
               if(val != '' && rv_email.test(val))
               {
                   $(this).css('background', '#A4FF83');
               }
               else
               {
                  alert("Error in filed Password!");
                  $(this).css('background', '#FF3C1D');
                  sub = false;
                  if (sub == false) {
                      sub = true;
                      $('#Password').val('');
                      $('#Password').css('background', '#fff');
                  } else {
                      $('#buttonsubmitreg').removeAttr('disabled');
                  }
               }
           break;

	   case 'ConfirmPassword':
              var rv_email = /^([a-zA-Z0-9_.-])/;
               if(val != '' && rv_email.test(val))
               {
                   $(this).css('background', '#A4FF83');
               }
               else
               {
                  alert("Error in filed Password!");
                  $(this).css('background', '#FF3C1D');
                  sub = false;
                  if (sub == false) {
                      sub = true;
                      $('#ConfirmPassword').val('');
                      $('#ConfirmPassword').css('background', '#fff');
                  } else {
                      $('#buttonsubmitreg').removeAttr('disabled');
                  }
               }
           break;

       } // end switch(...)

     }); // end blur()

     // “еперь отправим наше письмо с помощью AJAX
	


  }); // end script