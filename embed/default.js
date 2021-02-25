function toggleDiv(oDiv) {
	if (typeof (oDiv) !== "string")
		return;
	oDiv = document.getElementById(oDiv);
	if (oDiv.style.display === "block") {
		oDiv.style.display = "none";
		document.getElementById("ShowHide").innerHTML = `Show ${oDiv.id} >>>`;
	}
	else {
		oDiv.style.display = "block";
		document.getElementById("ShowHide").innerHTML = `<<< Hide ${oDiv.id}`;
	}
}

function BackToIndex() {
	var href = document.location.href;
	href = href.replace("display.aspx", "index.aspx");
	window.location = href;
	return false;
}

var playerinstance;

function SetTitle(sPath, sVideo) {
	if (sPath.length === 0 || sVideo.length === 0)
		return;

	document.title = sVideo;
	document.getElementById("title").innerHTML = sVideo;

	//var ext = getFilePathExtension(sVideo);
	//if (ext === "")
	//	sVideo += ".flv";

	//sVideo = "test.mp4";

	playerinstance = window.jwplayer("player");
	playerinstance.setup(
	{
		//file: "../" + sPath + "/" + sVideo,
		playlist: [{ 
			sources: [{ 
			//	file: "../" + sPath + "/" + sVideo + ".flv"
			//},{ 
				file: `../${sPath}/${sVideo}.mp4`
			}] 
		}], 
		//primary: "flash",
		volume: 80,
		width: 567,
		height: 475,
		stretching: "fill",
		skin: { name: "five" },
		autostart: true
		//,modes: [{
		//	type: "flash",
		//	src: "jwplayer.flash.swf"
		//}],
		//flashplayer: "jwplayer.flash.swf"
	});
}

function getFilePathExtension(path) {
	var filename = path.split("\\").pop().split("/").pop();
	return filename.substr((Math.max(0, filename.lastIndexOf(".")) || Infinity) + 1);
}

function OpenDialog() {
	if ($("#ShowHide").attr("value") === "Hide Lyrics") {
		$("#Lyrics").dialog("close");
	}
	else {
		$("#Lyrics").dialog("option", "position", [28, 61]);
		$("#Lyrics").dialog("option", "width", 260);
		$("#Lyrics").dialog("option", "height", 479);
		$("#Lyrics").dialog("option", "title", "Lyrics: "); // + $("h1").text()
		$("#Lyrics").dialog("open");
	}
	return false;
}

function file_name_only(str) {
	var slash = "/";
	if (str.match(/\\/))
		slash = "\\";
	return str.substring(str.lastIndexOf(slash) + 1, str.lastIndexOf("."));
}

function getElementsByClass(searchClass) {
	// This function returns an array of all HTML objects with the
	// specified className.  Tag is optional
	const returnArray = [];
	const els = document.getElementsByTagName("*");
	const pattern = new RegExp(`(^|\\s)${searchClass}(\\s|$)`);
	for (let i = 0; i < els.length; i++) {
		if (pattern.test(els[i].className))
			returnArray.push(els[i]);
	}
	return returnArray;
}

function QueryString(ji) {
	const hu = window.location.search.substring(1);
	const gy = hu.split("&");
	for (let i = 0; i < gy.length; i++) {
        const ft = gy[i].split("=");
        if (ft[0] === ji)
			return ft[1];
    }
    return hu;
}

function InitPol() {
	if (window.location.href.indexOf("path=videos/pol&") > 0) {
		document.getElementById("home").style.display = "none";
		document.getElementById("close").style.display = "none";
		document.getElementById("title").style.width = "100%";
		document.getElementById("Lyrics").style.display = "block";
		document.getElementById("ShowHide").innerHTML = " ";
	}
}

// // Set the zIndex to max + 5
// $("#Lyrics").maxZIndex({ inc: 5 });
// alert($.maxZIndex());
//
// /// <summary>
// /// Returns the max zOrder in the document (no parameter)
// /// Sets max zOrder by passing a non-zero number
// /// which gets added to the highest zOrder.
// /// </summary>
// /// <param name="opt" type="object">
// /// inc: increment value,
// /// group: selector for zIndex elements to find max for
// /// </param>
// /// <returns type="jQuery" />
//$.maxZIndex = $.fn.maxZIndex = function(opt)
//{
//	var def = { inc: 10, group: "*" };
//	$.extend(def, opt);
//	var zmax = 0;
//	$(def.group).each(function() {
//		var cur = parseInt($(this).css('z-index'));
//		zmax = cur > zmax ? cur : zmax;
//	});
//	if (!this.jquery)
//		return zmax;
//	return this.each(function() {
//		zmax += def.inc;
//		$(this).css("z-index", zmax);
//	});
//}

function popVideo(vid, darken) {
	// This function accepts a division ID (vid), either a string or the actual
	// object itself.   vid is mandatory.   darken is optional, if it's true
	// the page will be greyed out under the video.
	var videos = getElementsByClass('video'); // Get all the videos on the page.
	if (videos === "undefined") {
		alert("Cannot find element with a class=video");
		return false;
	}
	var isplaying = null;
	for (var i = 0; i < videos.length; i++) {
		// Loop through all the videos
		if (videos[i].style.display === "block") {
			// This video is playing
			isplaying = videos[i].id; 		// remember its name
			var tmp = videos[i].innerHTML; 	// Get the division contents
			videos[i].innerHTML = ""; 		// destroy the contents
			videos[i].style.display = "none"; // Terminate the video.
			videos[i].innerHTML = tmp; 		// rebuild the contents.
		}
	}
	// RAS error occurs here!
	// This handles the darkening of the background while a video is playing.
	// First we see if the dark layer exists.
	var dark = document.getElementById('darkenScreenObject');
	if (!dark) {
		// The dark layer doesn't exist, it's never been created.  So we'll
		// create it here and apply some basic styles.
		var tbdy = document.getElementsByTagName("body")[0];
		var tnode = document.createElement("div"); 	// Create the layer.
		tnode.style.backgroundColor = "rgb(0, 0, 0)"; // Make it black.
		tnode.style.opacity = "0.7"; 					// Set the opacity (firefox/Opera)
		tnode.style.MozOpacity = "0.70"; 				// Firefox 1.5
		tnode.style.filter = "alpha(opacity=40)"; 	// IE.
		tnode.style.zIndex = "1"; 					// Zindex of 50 so it "floats"
		tnode.style.position = "absolute"; 			// Position absolutely
		tnode.style.top = "0px"; 					// In the top
		tnode.style.left = "0px"; 					// Left corner of the page
		tnode.style.overflow = "hidden"; 			// Try to avoid making scroll bars
		tnode.style.display = "none"; 				// Start out Hidden
		tnode.id = "darkenScreenObject"; 			// Name it so we can find it later
		tbdy.appendChild(tnode); 					// Add it to the web page
		dark = document.getElementById("darkenScreenObject"); // Get the object.
	}
	dark.style.display = "none";
	if ((isplaying === vid) || (/^close$/i.test(vid))) {
		return false;
	}
	if (typeof (vid) == "string") {
		vid = document.getElementById(vid);
	}
	if (vid && typeof (vid) == "object") {
		if (darken) {
			var pageWidth;
			var pageHeight;
			// Calculate the page width and height
			if (window.innerHeight && window.scrollMaxY) {
				pageWidth = window.innerWidth + window.scrollMaxX;
				pageHeight = window.innerHeight + window.scrollMaxY;
			}
			else if (document.body.scrollHeight > document.bdy.offsetHeight) {
				pageWidth = document.body.scrollWidth;
				pageHeight = document.body.scrollHeight;
			}
			else {
				pageWidth = document.body.offsetWidth + document.body.offsetLeft;
				pageHeight = document.body.offsetHeight + document.body.offsetTop;
			}
			//set the shader to cover the entire page and make it visible.
			dark.style.width = pageWidth + "px";
			dark.style.height = pageHeight + "px";
			dark.style.display = "block";
		}
		// Make the video visible and set the zindex so its on top of everything else
		vid.style.zIndex = "100";
		vid.style.display = "block";
		var scrollTop = 0;
		if (document.documentElement && document.documentElement.scrollTop)
			scrollTop = document.documentElement.scrollTop;
		else if (document.bdy)
			scrollTop = document.bdy.scrollTop;
		// set the starting x and y position of the video
		vid.style.top = scrollTop + Math.floor((document.documentElement.clientHeight / 2) - (vid.offsetHeight / 2)) + "px";
		vid.style.left = Math.floor((document.documentElement.clientWidth / 2) - (vid.offsetWidth / 2)) + "px";
	}
	return false;
}



