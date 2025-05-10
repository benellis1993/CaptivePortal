document.addEventListener("DOMContentLoaded", function() {
    // Get references to the elements
    const emailInput = document.getElementById('UserEmail');
    const emailError = document.getElementById('email-error');
    const termsCheckbox = document.getElementById('termsCheckbox');
    const connectButton = document.getElementById('connectButton');
    const form = document.querySelector('.login-form');

    // Function to validate email format
    function isValidEmail(email) {
        return /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(email);
    }

    // Function to update the button's disabled state based on email and checkbox
    function updateButtonState() {
        const emailValid = isValidEmail(emailInput.value.trim());
        const termsChecked = termsCheckbox.checked;

        if (termsChecked && emailValid) {
            connectButton.disabled = false;
            connectButton.title = ""; 
        } else {
            connectButton.disabled = true;
            connectButton.title = "Accept Terms of Service and enter a valid email to continue."; 
        }
    }

    // Add event listener for email input - validates as user types
    emailInput.addEventListener('input', function() {
        if (!isValidEmail(emailInput.value.trim())) {
            // Check if emailError element exists before trying to use it (safer check)
             if (emailError) {
                emailError.textContent = "Please enter a valid email address (e.g., user@example.com).";
            }
            emailInput.classList.add('is-invalid');
        } else {
             if (emailError) {
                emailError.textContent = ""; 
            }
            emailInput.classList.remove('is-invalid');
        }
        updateButtonState(); // Always update button state after input changes
    });

    // Add event listener for terms checkbox change - updates button state
    termsCheckbox.addEventListener('change', updateButtonState);

    // Add event listener for form submission - prevents submission if email is invalid
    form.addEventListener('submit', function(event) {
        // Final check on email validity before submitting
        if (!isValidEmail(emailInput.value.trim())) {
            // Display error message and highlight field
             if (emailError) {
                emailError.textContent = "Please enter a valid email address (e.g., user@example.com).";
             }
            emailInput.classList.add('is-invalid');
            event.preventDefault();
        }
    });
    updateButtonState();
});