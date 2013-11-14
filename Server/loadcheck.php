<?php
function check(){

echo '<form name="lar"><input type="text" readonly size="30" name="STATUS" value="Laden..." style="background-Color:#ffa500; color:green; font-size:30; position:absolute; width:400px; visibility:hidden">
<a name="Alle" style ="position:absolute; top:-30px; left:-30px;" href="javascript:history.back()"><font color="blue" size="3">zurück</font><font size="3"> (History)</font></a>
<a name="Blle" style ="position:absolute; top:-30px; left:-30px;" href="kodieren.php"><font color="blue" size="3">zurück</font><font size="3"> (Hauptmenü)</font></a>
</form>';
echo '<script src="check.js" type="text/javascript"></script>';
}

function set_ltext($text){
echo "<script type='text/javascript'>document.lar.STATUS.value='$text'</script>";
 

}
?>