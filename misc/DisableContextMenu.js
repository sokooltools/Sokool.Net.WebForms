var MSG_ACTION_DISABLED='Popup menu is blocked!';

function stopEvent(ev){
	if(document.all){
		ev.cancelBubble = true;
		ev.returnValue = false;
	}else{
		window.captureEvents(ev.KEYDOWN);
		window.captureEvents(ev.MOUSEDOWN);
		ev.preventDefault();
		ev.stopPropagation();
	}
}

function keydown(e){;
	if (document.all){
		if((event.shiftKey&&event.keyCode==121) || (event.keyCode==93) || (event.button == 2)){
			alert(MSG_ACTION_DISABLED);
			return false;
		}
	}else{
		if((e.shiftKey&&e.keyCode==121) || (e.keyCode==93) || (e.which==3)){
			stopEvent(e);
			alert(MSG_ACTION_DISABLED);
			return false;
		}
	}
	if(!document.all) {
		window.releaseEvents(e.KEYDOWN);
		window.releaseEvents(e.MOUSEDOWN);
	}
	return true;
}
if (document.all)
{
	document.onmousedown=keydown;
}
else
{
	document.onclick=keydown;
}
document.onkeydown=keydown;