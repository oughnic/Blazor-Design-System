/**
 * GOV.UK Design System Blazor - Accessibility JavaScript Helpers
 * These functions support WCAG 2.2 compliant accessibility features
 */

window.GovUkDesignSystem = window.GovUkDesignSystem || {};

/**
 * Focus an element by selector
 * @param {string} selector - CSS selector for the element to focus
 */
window.GovUkDesignSystem.focusElement = function (selector) {
    const element = document.querySelector(selector);
    if (element) {
        element.focus();
    }
};

/**
 * Focus the error summary component
 */
window.GovUkDesignSystem.focusErrorSummary = function () {
    const errorSummary = document.querySelector('[data-module="govuk-error-summary"]');
    if (errorSummary) {
        errorSummary.focus();
    }
};

/**
 * Navigate to a form field from an error summary link
 * This scrolls the associated legend or label into view before focusing the input
 * to ensure the user sees the context of the field
 * @param {string} fieldId - The ID of the form field to navigate to
 */
window.GovUkDesignSystem.navigateToField = function (fieldId) {
    if (!fieldId || typeof fieldId !== 'string') return;

    const input = document.getElementById(fieldId);
    if (!input) return;

    // Find associated legend or label
    const fieldset = input.closest('fieldset');
    let legendOrLabel = null;

    if (fieldset) {
        const legends = fieldset.getElementsByTagName('legend');
        if (legends.length > 0) {
            const candidateLegend = legends[0];

            // For radio/checkbox inputs, always use the legend
            if (input.type === 'checkbox' || input.type === 'radio') {
                legendOrLabel = candidateLegend;
            } else {
                // For other inputs, use legend if input would be in top half of screen
                const legendTop = candidateLegend.getBoundingClientRect().top;
                const inputRect = input.getBoundingClientRect();

                if (inputRect.height && window.innerHeight) {
                    const inputBottom = inputRect.top + inputRect.height;
                    if (inputBottom - legendTop < window.innerHeight / 2) {
                        legendOrLabel = candidateLegend;
                    }
                }
            }
        }
    }

    // Fall back to label - use the already validated input element to find its label
    if (!legendOrLabel) {
        const labelElement = document.querySelector('label[for="' + CSS.escape(fieldId) + '"]');
        legendOrLabel = labelElement || input.closest('label');
    }

    // Scroll legend/label into view, then focus input
    if (legendOrLabel) {
        legendOrLabel.scrollIntoView();
    }

    input.focus({ preventScroll: true });
};
