function toggleDiv(oDiv)
{
    if (typeof (oDiv) == "string")
    {
        oDiv = document.getElementById(oDiv);
        if (oDiv.style.display == 'block')
        {
            oDiv.style.display = 'none';
            document.getElementById("ShowHide").innerHTML = "Show " + oDiv.id + " >>>";
        }
        else
        {
            oDiv.style.display = 'block';
            document.getElementById("ShowHide").innerHTML = "<<< Hide " + oDiv.id;
        }
    }
}

function BackToIndex()
{
    var href = document.location.href;
    href = href.replace("display.aspx", "index.aspx");
    window.location = href;
    return false;
}

function SetTitle(sPath, sVideo)
{
    //var sPath = QueryString('path');
    //var sVideo = QueryString('video');

    if (sPath.length == 0 || sVideo.length == 0) return;

    document.title = sVideo;
    document.getElementById("title").innerHTML = sVideo;

    //var so = new SWFObject('embed/mediaplayer.swf', 'mpl', '420', '352', '8');
    var so = new SWFObject('../embed/player-viral.swf', 'ply', '567', '475', '9');
    so.addParam('allowscriptaccess', 'always');
    so.addParam('allowfullscreen', 'true');
    so.addParam('wmode', 'opaque');
    //so.addVariable('width', '630');
    //so.addVariable('height', '528');
    so.addVariable("file", "../" + sPath + "/" + sVideo + ".flv");
    so.addVariable('lightcolor', '0x333333');
    so.addVariable('screencolor', '0x999999');
    so.addVariable('showstop', 'true');
    so.addVariable('showdownload', 'false');
    so.addVariable('autostart', 'true');
    so.addVariable('stretching', 'uniform');
    so.addVariable('skin','../embed/beelden/beelden.xml');
    so.write('player');
}

function file_name_only(str)
{
    var slash = '/'
    if (str.match(/\\/))
        slash = '\\'
    return str.substring(str.lastIndexOf(slash) + 1, str.lastIndexOf('.'))
}

function getElementsByClass(searchClass)
{
    // This function returns an array of all HTML objects with the
    // specified className.  Tag is optional
    var returnArray = [];
    var els = document.getElementsByTagName('*');
    var pattern = new RegExp('(^|\\s)' + searchClass + '(\\s|$)');
    for (var i = 0; i < els.length; i++)
    {
        if (pattern.test(els[i].className))
        {
            returnArray.push(els[i]);
        }
    }
    return returnArray;
}

function QueryString(ji)
{
    hu = window.location.search.substring(1);
    gy = hu.split("&");
    for (i = 0; i < gy.length; i++)
    {
        ft = gy[i].split("=");
        if (ft[0] == ji)
        {
            return ft[1];
        }
    }
    return hu;
}

// // Set the zIndex to max + 5
// $("#Lyrics").maxZIndex({ inc: 5 });
// alert($.maxZIndex());
//
$.maxZIndex = $.fn.maxZIndex = function(opt)
{
	/// <summary>
	/// Returns the max zOrder in the document (no parameter)
	/// Sets max zOrder by passing a non-zero number
	/// which gets added to the highest zOrder.
	/// </summary>
	/// <param name="opt" type="object">
	/// inc: increment value,
	/// group: selector for zIndex elements to find max for
	/// </param>
	/// <returns type="jQuery" />
	var def = { inc: 10, group: "*" };
	$.extend(def, opt);
	var zmax = 0;
	$(def.group).each(function() {
		var cur = parseInt($(this).css('z-index'));
		zmax = cur > zmax ? cur : zmax;
	});
	if (!this.jquery)
		return zmax;

	return this.each(function() {
		zmax += def.inc;
		$(this).css("z-index", zmax);
	});
}

function popVideo(vid, darken)
{
    // This function accepts a division ID (vid), either a string or the actual
    // object itself.   vid is mandatory.   darken is optional, if it's true
    // the page will be greyed out under the video.
    var videos = getElementsByClass('video'); // Get all the videos on the page.
    if (videos == 'undefined')
    {
        alert('Cannot find element with a class=video');
        return false;
    }
    var isplaying = null;
    for (i = 0; i < videos.length; i++)
    {
        // Loop through all the videos
        if (videos[i].style.display == 'block')
        {
            // This video is playing
            isplaying = videos[i].id; 		// remember its name
            var tmp = videos[i].innerHTML; 	// Get the division contents
            videos[i].innerHTML = ''; 		// destroy the contents
            videos[i].style.display = 'none'; // Terminate the video.
            videos[i].innerHTML = tmp; 		// rebuild the contents.
        }
    }
    // RAS error occurs here!
    // This handles the darkening of the background while a video is playing.
    // First we see if the dark layer exists.
    var dark = document.getElementById('darkenScreenObject');
    if (!dark)
    {
        // The dark layer doesn't exist, it's never been created.  So we'll
        // create it here and apply some basic styles.
        var tbdy = document.getElementsByTagName("bdy")[0];
        var tnode = document.createElement('div'); 	// Create the layer.
        tnode.style.backgroundColor = 'rgb(0, 0, 0)'; // Make it black.
        tnode.style.opacity = '0.7'; 					// Set the opacity (firefox/Opera)
        tnode.style.MozOpacity = '0.70'; 				// Firefox 1.5
        tnode.style.filter = 'alpha(opacity=40)'; 	// IE.
        tnode.style.zIndex = '1'; 					// Zindex of 50 so it "floats"
        tnode.style.position = 'absolute'; 			// Position absolutely
        tnode.style.top = '0px'; 					// In the top
        tnode.style.left = '0px'; 					// Left corner of the page
        tnode.style.overflow = 'hidden'; 			// Try to avoid making scroll bars
        tnode.style.display = 'none'; 				// Start out Hidden
        tnode.id = 'darkenScreenObject'; 			// Name it so we can find it later
        tbdy.appendChild(tnode); 					// Add it to the web page
        dark = document.getElementById('darkenScreenObject'); // Get the object.
    }
    dark.style.display = 'none';
    if ((isplaying == vid) || (/^close$/i.test(vid)))
    {
        return false;
    }
    if (typeof (vid) == "string")
    {
        vid = document.getElementById(vid);
    }
    if (vid && typeof (vid) == "object")
    {
        if (darken)
        {
            var pageWidth;
            var pageHeight;
            // Calculate the page width and height
            if (window.innerHeight && window.scrollMaxY)
            {
                pageWidth = window.innerWidth + window.scrollMaxX;
                pageHeight = window.innerHeight + window.scrollMaxY;
            }
            else if (document.bdy.scrollHeight > document.bdy.offsetHeight)
            {
                pageWidth = document.bdy.scrollWidth;
                pageHeight = document.bdy.scrollHeight;
            }
            else
            {
                pageWidth = document.bdy.offsetWidth + document.bdy.offsetLeft;
                pageHeight = document.bdy.offsetHeight + document.bdy.offsetTop;
            }
            //set the shader to cover the entire page and make it visible.
            dark.style.width = pageWidth + 'px';
            dark.style.height = pageHeight + 'px';
            dark.style.display = 'block';
        }
        // Make the video visible and set the zindex so its on top of everything else
        vid.style.zIndex = '100';
        vid.style.display = 'block';
        var scrollTop = 0;
        if (document.documentElement && document.documentElement.scrollTop)
            scrollTop = document.documentElement.scrollTop;
        else if (document.bdy)
            scrollTop = document.bdy.scrollTop;
        // set the starting x and y position of the video
        vid.style.top = scrollTop + Math.floor((document.documentElement.clientHeight / 2) - (vid.offsetHeight / 2)) + 'px';
        vid.style.left = Math.floor((document.documentElement.clientWidth / 2) - (vid.offsetWidth / 2)) + 'px';
    }
    return false;
}



