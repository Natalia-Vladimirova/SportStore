 $(document).ready(function(){
	
     
var sub = true;
$('#buttonsubmitreg').attr('disabled');
     $('#UserName, #Email, #Password,#ConfirmPassword').unbind().blur( function(){

        // ��� �������� ���������� ��������� � �������� � �������� ������� ���� � ����������
         var id = $(this).attr('id');
         var val = $(this).val();

       // ����� ����, ��� ���� �������� �����, ���������� �������� id, ����������� � id ������� ����
       switch(id)
       {
             // �������� ���� "���"
             case 'UserName':
                var rv_name = /^[a-zA-Z�-��-�]+$/; // ���������� ���������� ���������

                // E��� ����� ����� ������ 2 ��������, ��� �� ������ � ������������� ���. ���������,
                // �� ��������� ����� ���� ����� .not_error,
                // � ���� � ��������� ��� ������ ������� ����� " �������", �.�. ��������� ��� ����� ���� �������� �������

                if(val.length > 1 && val != '' && rv_name.test(val))
                {
                    $(this).css('background', '#A4FF83');

                }

              // �����, �� ������� ����� not-error � �������� ��� �� ����� error, ������ � ��� ��� ���� �������� ������ ���������,
              // � ���� � ��� ��������� ������� ��������� �� ������ � ��������� ��� ������ ���������

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

           // �������� email
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


          // �������� ���� "���������"
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

     // ������ �������� ���� ������ � ������� AJAX
	


  }); // end script