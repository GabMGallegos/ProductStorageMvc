import './styles.css';

function openModal(modalId) {
    const modal = document.getElementById(modalId);

    if (modal) {
        modal.classList.add('modal-visible');
    }
}

function closeModal(modalId) {
    const modal = document.getElementById(modalId);

    if (modal) {
        modal.classList.remove('modal-visible');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const editButtons = document.querySelectorAll('.open-edit-modal');
    const deleteButtons = document.querySelectorAll('.open-delete-modal');
    const closeButtons = document.querySelectorAll('[data-close-modal]');

    editButtons.forEach((button) => {
        button.addEventListener('click', () => {
            document.getElementById('editProductId').value = button.dataset.id;
            document.getElementById('editProductName').value = button.dataset.name;
            document.getElementById('editProductPrice').value = button.dataset.price;
            document.getElementById('editProductQuantity').value = button.dataset.quantity;

            openModal('editModal');
        });
    });

    deleteButtons.forEach((button) => {
        button.addEventListener('click', () => {
            document.getElementById('deleteProductId').value = button.dataset.id;

            document.getElementById('deleteProductIdText').textContent = button.dataset.id;
            document.getElementById('deleteProductNameText').textContent = button.dataset.name;
            document.getElementById('deleteProductPriceText').textContent = button.dataset.price;
            document.getElementById('deleteProductQuantityText').textContent = button.dataset.quantity;
            document.getElementById('deleteProductTotalText').textContent = button.dataset.total;

            openModal('deleteModal');
        });
    });

    closeButtons.forEach((button) => {
        button.addEventListener('click', () => {
            closeModal(button.dataset.closeModal);
        });
    });

    document.querySelectorAll('.modal-overlay').forEach((modal) => {
        modal.addEventListener('click', (event) => {
            if (event.target === modal) {
                modal.classList.remove('modal-visible');
            }
        });
    });
});