function getCookie(name) {
    // Create a name=value pair to search for
    const nameEQ = name + "=";
    // Split the document.cookie string into individual cookies
    const cookies = document.cookie.split(';');

    // Loop through all cookies
    for (let i = 0; i < cookies.length; i++) {
        let c = cookies[i];
        // Trim leading spaces
        while (c.charAt(0) === ' ') c = c.substring(1);
        // Check if this cookie starts with the desired name
        if (c.indexOf(nameEQ) === 0) {
            // Return the cookie's value
            return c.substring(nameEQ.length, c.length);
        }
    }

    // Return null if the cookie was not found
    return null;
}

/**
 * Function to handle route changes and log specific cookie values.
 */
function onUserAction() {
    // Get the value of the specific cookie by name
    const cookieName = 'IdentityCookie'; // Change this to the name of your cookie
    const cookieValue = getCookie(cookieName);

    // Log the value of the cookie or a message if not found
    if (cookieValue) {
        console.log(`Value of ${cookieName}:`, cookieValue);
        return cookieValue;
    } else {
        console.log(`Cookie with name ${cookieName} not found.`);
    }

    // Optionally log all cookies for debugging purposes
    console.log('All cookies:', document.cookie);

}