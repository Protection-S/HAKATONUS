<<template>
    <div class="background">
        <div class="overlay"></div>
        <img src="../assets/background.gif" alt="Background" class="background-image">
        <div class="container">
            <div v-if="!isLoggedIn">
                <h1 class="title typing-animation">Требуется авторизация</h1>
                <div class="buttons">
                    <button @click="toggleLoginForm">Вход</button>
                    <button @click="toggleRegisterForm">Регистрация</button>
                </div>
                <div :class="{ 'form-container': true, 'active': showLoginForm }" id="loginForm">
                    <form @submit.prevent="login">
                        <input type="email" class="text-black" placeholder="Email" v-model="user.mail" required>
                        <input type="password" class="text-black" placeholder="Password" v-model="user.password" required>
                        <button type="submit">Войти</button>
                    </form>
                </div>
                <div :class="{ 'form-container': true, 'active': showRegisterForm }" id="registerForm">
                    <form @submit.prevent="register">
                        <input type="email" class="text-black"  placeholder="Email" v-model="user.mail" required>
                        <input type="password" class="text-black" placeholder="Password" v-model="user.password" required>
                        <input type="password" class="text-black" placeholder="Confirm Password" v-model="user.confirmPassword" required>
                        <input type="text" class="text-black" placeholder="First Name" v-model="user.firstName" required>
                        <input type="text" class="text-black" placeholder="Last Name" v-model="user.lastName" required>
                        <button type="submit">Создать аккаунт</button>
                    </form>
                </div>
            </div>
            <div v-else>
                <h1 class="title typing-animation">Authorized</h1>
                <button @click="logout">Exit</button>
            </div>
        </div>
    </div>
</template>



<script>
    import { ref } from "vue";
    import { gsap } from "gsap";
    import axios from 'axios';

    export default {
        data() {
            return {
                isLoggedIn: false,
                showLoginForm: ref(false),
                showRegisterForm: ref(false),
                user: {
                    mail: '',
                    password: '',
                    confirmPassword: '',
                    firstName: '',
                    lastName: ''
                }
            };
        },

        methods: {
            toggleLoginForm() {
                this.showLoginForm = true;
                this.showRegisterForm = false;
                document.querySelector('.overlay').classList.add('active');
            },

            toggleRegisterForm() {
                this.showLoginForm = false;
                this.showRegisterForm = true;
                document.querySelector('.overlay').classList.add('active');
            },

            async login() {
                if (!this.user.mail || !this.user.password) {
                    alert('Please fill in all fields.');
                    return;
                }
                if (!this.user.mail.includes('@')) {
                    alert('Please enter a valid email address.');
                    return;
                }
                if (this.user.password.length < 8) {
                    alert('Password must be at least 8 characters long.');
                    return;
                }

                try {
                    const response = await axios.post('https://localhost:7175/api/AuthProcess/login', {
                        name: this.user.firstName,
                        lastname: this.user.lastName,
                        email: this.user.mail,
                        password: this.user.password
                    });
                    localStorage.setItem('mail', this.user.mail);
                    this.$router.push('/main-title');
                    this.isLoggedIn = true;
                } catch (error) {
                    console.error('Ошибка входа:', error.response.data.message);
                }
            },

            async register() {
                if (!this.user.mail || !this.user.password) {
                    alert('Please fill in all fields.');
                    return;
                }
                if (!this.user.mail.includes('@')) {
                    alert('Please enter a valid email address.');
                    return;
                }
                if (this.user.password.length < 8) {
                    alert('Password must be at least 8 characters long.');
                    return;
                }

                try {
                    const response = await axios.post('https://localhost:7175/api/AuthProcess/register', {
                        name: this.user.firstName,
                        lastname: this.user.lastName,
                        email: this.user.mail,
                        password: this.user.password
                    });
                    console.log('Успешная регистрация:', response.data);
                } catch (error) {
                    console.error('Ошибка регистрации:', error.response.data.message);
                }
            },


            logout() {
                localStorage.removeItem('token');
                this.isLoggedIn = false;
                location.reload();
            }
        },

        mounted() {
            if (localStorage.getItem('token')) {
                this.isLoggedIn = true;
            }
            gsap.from('.title', { opacity: 0, duration: 1, y: -50, delay: 0.5 });
            gsap.from('.buttons button', { opacity: 0, duration: 1, y: 50, delay: 1 });
            gsap.to('.title', {
                y: -10,
                repeat: -1,
                yoyo: true,
                duration: 1.5,
                ease: 'power1.inOut',
                delay: 2
            });

            gsap.from('.header', { opacity: 0, duration: 1, delay: 0.5, x: 50 });
        }
    };
</script>

<style scoped src="../styles/main.css"></style>
