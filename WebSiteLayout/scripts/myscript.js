function sendMe(){
    var name=document.getElementById("txtName").value;
    var email=document.getElementById("txtEmail").value;
    var message=document.getElementById("txtMessage").value;
    alert(`Hi,${name}\nWe received your sending message with this ${email}`);
}
function showMe(){
    $("#showAuthorEmail").show('slow');
}