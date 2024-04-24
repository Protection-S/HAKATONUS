<template>
    <div>
        <header class="header">
            <div class="header-background">
            </div>
            <nav class="navigation">
                <div class="navigation-buttons">
                    <button class="navigation-button" @click="openProject"><img src="../assets/rating-ico.png" alt="Rating Icon" class="button-icon">Рейтинг компаний</button>
                    <button class="navigation-button" @click="openCommand"><img src="../assets/search-ico.png" alt="Search Icon" class="button-icon">Поиск команды</button>
                    <button class="navigation-button" @click="openStartup"><img src="../assets/startup-ico.png" alt="Startup Icon" class="button-icon">Стартапы</button>
                </div>
                <div class="account-button">
                    <button class="navigation-button" @click="toggleModal">Аккаунт</button>
                </div>
            </nav>
        </header>

        <div v-if="showModal" class="modal-backdrop" @click="toggleModal">
            <div class="modal-content" @click.stop>
                <div class="sidebar">
                    <button class="sidebar-button" @click="switchTab('profile')">Профиль</button>
                    <button class="sidebar-button" @click="switchTab('privacy')">Безопасность</button>
                </div>
                <div class="content">
                    <div v-if="currentTab === 'profile'">
                        <h3>Профиль</h3>
                        <div class="input-group">
                            <label for="firstName">Имя:</label>
                            <input class="text-black" type="text" id="firstName" v-model="user.firstName">
                        </div>
                        <div class="input-group">
                            <label for="lastName">Фамилия:</label>
                            <input class="text-black" type="text" id="lastName" v-model="user.lastName">
                        </div>
                        <div class="input-group">
                            <label for="email">Почта:</label>
                            <input class="text-black"  type="email" id="email" v-model="user.email">
                        </div>
                    </div>
                    <div v-if="currentTab === 'privacy'">
                        <h3>Смена пароля</h3>
                        <form @submit.prevent="updatePassword">
                            <div class="input-group">
                                <label for="newPassword">Новый пароль:</label>
                                <input class="text-black" type="password" id="newPassword" v-model="user.newPassword">
                            </div>
                            <button type="submit" @click="changePass">Изменить пароль</button>
                        </form>
                    </div>
                </div>
                <button class="logout-button" @click="logout">Выйти</button>
            </div>
        </div>
    </div>

    <main>
        <component :is="currentComponent"></component>
    </main>
</template>

<script>
    import axios from 'axios';
    import Command from './Commands.vue';
    import Startup from './Startups.vue';
    import Project from './Projects.vue';



    export default {
        data() {
            return {
                currentComponent: Project,
                showModal: false,
                modalTimeline: null,
                currentTab: 'profile',
                user: {
                    firstName: '',
                    lastName: '',
                    email: '',
                    newPassword: ''
                }
            }
        },
        methods: {
            toggleModal() {
                this.showModal = !this.showModal;
                if (this.showModal) {
                    this.openAnimation();                    
                } else {
                    this.closeAnimation();
                }
            },

            openCommand() {
                this.currentComponent = Command;
            },
            openProject() {
                this.currentComponent = Project;
            },
            openStartup() {
                this.currentComponent = Startup;
            },

            openAnimation() {
                this.modalTimeline = gsap.timeline()
                    .from(".modal-content", { scale: 0.5, opacity: 0, duration: 0.5, ease: "back.out(1.7)" });

            },
            closeAnimation() {
                if (this.modalTimeline) {
                    this.modalTimeline.reverse();
                }
            },
            logout() {
                localStorage.removeItem('token');
                this.showModal = false;
                localStorage.removeItem('mail');
                this.$router.push('/');
            },
            switchTab(tab) {
                this.currentTab = tab;
            },
            async updatePassword() {
                try {
                    const response = await axios.put(`https://localhost:7175/api/AuthProcess/updatePassword/${this.user.email}`, {
                        NewPassword: this.user.newPassword
                    });
                    console.log("Пароль обновлен на: ", this.user.newPassword);
                    this.user.newPassword = '';
                    alert("Пароль успешно изменен.");
                } catch (error) {
                    console.error('Ошибка обновления пароля:', error.response.data.message);
                }
            },
            async getUserData() {
                try {
                    const response = await axios.get(`https://localhost:7175/api/AuthProcess/getUserData/${this.user.email}`);
                    this.user.firstName = response.data.name;
                    this.user.lastName = response.data.lastName;
                    this.user.email = response.data.email
                } catch (error) {
                    console.error('Ошибка получения данных:', error.response.data.message);
                }
            }
        },
        async mounted() {
            if (localStorage.getItem('mail')) {
                this.user.email = localStorage.getItem('mail');
                this.isLoggedIn = true;
                this.getUserData();
            }
        }
    }
</script>

<style scoped>
    .header {
        position: relative;
        background-color: #1D472A;
        height: 207px;
    }

    .header-background {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        overflow: hidden;
        background: url('../assets/satoru.png') no-repeat center center; 
        background-size: cover;
        background-repeat: no-repeat;
    }


    .header-photo {
        width: 100%;
        height: auto;
    }

    .navigation {
        position: absolute;
        bottom: 0;
        left: 0;
        width: 100%;
        background-color: #1D472A;
        height: 60px;
        display: flex;
        justify-content: center;
        align-items: center;
        padding: 0 20px;
    }

    .navigation-buttons {
        display: flex;
        align-items: center;
    }

    .navigation-button {
        background-color: transparent;
        border: none;
        background: #3C763D;
        height: 37px;
        color: white;
        border-radius: 8px;
        font-size: 16px;
        cursor: pointer;
        display: flex;
        align-items: center;
        padding: 0 15px;
        margin-right: 10px;
        transition: transform 0.5s;
    }

    .account-button {
        position: absolute;
        right: 50px;
    }

    .navigation-button:last-child {
        margin-right: 0;
    }

    .navigation-button:hover {
        text-decoration: underline;
        transform: translateY(-5px);
    }

    .button-icon {
        margin-right: 8px;
        width: 20px;
        height: auto;
    }

    .modal-backdrop {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 100;
    }

    .modal-content {
        background-color: #1D472A;
        display: flex;
        width: 60%;
        border-radius: 10px;
        overflow: hidden;
    }

    .sidebar {
        background-color: #2E5940;
        width: 25%;
        padding: 20px;
        display: flex;
        flex-direction: column;
    }

    .sidebar-button {
        background: none;
        border: none;
        color: white;
        padding: 10px;
        cursor: pointer;
        text-align: left;
        border-bottom: 1px solid #3C763D;
    }

        .sidebar-button:hover {
            background-color: #3C763D;
        }

    .content {
        padding: 20px;
        color: white;
        width: 75%;
    }

    .input-group {
        margin-bottom: 15px;
    }

    label {
        display: block;
        font-weight: bold;
        margin-bottom: 5px;
        color: #fff;
    }

    input[type="text"],
    input[type="password"],
    input[type="email"] {
        width: 100%;
        padding: 8px;
        border-radius: 5px;
        border: 1px solid #ccc;
        background-color: #f8f8f8;
        transition: border-color 0.3s ease;
    }

        input[type="text"]:focus,
        input[type="password"]:focus,
        input[type="email"]:focus {
            outline: none;
            border-color: #3C763D;
        }

    .logout-button {
        background-color: #d9534f;
        color: #fff;
        border: none;
        border-radius: 5px;
        padding: 8px 15px;
        cursor: pointer;
        transition: background-color 0.3s ease;
    }

        .logout-button:hover {
            background-color: #c9302c;
        }
</style>
