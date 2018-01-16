$(document).ready(function() {  
console.log("ok") 
    $("#formId").submit(function(event) {  
        console.log("it is intercept form") 
        event.preventDefault();  
        $.ajax({  
            type: "POST",    
            url: '@Url.Action("AddProduct", "Products")',  
            success: function(result) {    
                onAjaxRequestSuccess(result);  
            },  
            error: function(jqXHR, textStatus, errorThrown) {  
                //do your own thing  
                alert("fail");  
            }  
        });  
    }); //end .submit()  
});  
var onAjaxRequestSuccess = function(result) {  
    console.log("it is working")
    if (result.EnableError) {  
        // Setting.  
        alert(result.ErrorMsg);  
    } else if (result.EnableSuccess) {  
        // Setting.  
        alert(result.SuccessMsg);  
        // Resetting form.  
        $('#AjaxformId').get(0).reset();  
    }  
}  
