<template>
    <div class="bg-gray-200 h-12 flex items-center justify-center relative">
        <div ref="titleWrapper">
            <span class="font-semibold text-xl">Список компаний</span>
        </div>
    </div>

    <div class="container mx-auto p-4 sm:p-6">
        <div v-for="(company, index) in companies" :key="index" class="flex flex-wrap items-start space-x-4 pb-2 pt-2 border-b-2">
            <div class="w-full sm:w-auto">
                <div class="font-semibold text-xl bg-green-800 text-white p-2">
                    Место
                </div>
                <div class="text-lg text-center m-auto">{{ index + 1 }}</div>
            </div>
            <div class="w-full sm:w-auto flex-1">
                <div class="font-semibold text-xl mb-2 sm:mb-4 bg-green-800 text-white p-2">
                    {{ company.name }}
                </div>
                <div class="text-sm text-gray-700 px-2 pb-2 sm:pb-4 border-b-2 border-green-800 bg-white">
                    {{ company.description }}
                </div>
                <div class="flex flex-col sm:flex-row mt-2 sm:mt-4 space-y-2 sm:space-y-0 space-x-0 sm:space-x-4">
                    <div class="flex items-center border-b-2">
                        <img src="../icons/history.png" alt="Иконка истории" class="h-6 mr-2">
                        <div>{{ company.date }}</div>
                    </div>
                    <div class="flex items-center border-b-2">
                        <img src="../icons/view.png" alt="Иконка просмотров" class="h-6 mr-2">
                        <div>{{ company.views }}</div>
                    </div>
                    <div class="flex items-center border-b-2">
                        <img src="../icons/comment.png" alt="Иконка комментариев" class="h-6 mr-2">
                        <div>{{ company.comments }}</div>
                    </div>
                </div>

                <div class="bg-green-800 justify-center text-white p-2 mt-2 sm:mt-2 flex m-2">
                    <button @click="openModal(index)">Подробная информация</button>
                </div>
            </div>
            <div class="w-full sm:w-auto">
                <div class="font-semibold text-xl bg-green-800 text-white p-2">
                    Специализация
                </div>
                <div class="text-sm">
                    <div class="font-semibold mt-1">Тип компании:</div>
                    <div>{{ company.companyType }}</div>
                    <div class="font-semibold mt-1">Виды услуг:</div>
                    <div>{{ company.services }}</div>
                </div>
            </div>
            <div class="w-full sm:w-auto">
             <img src="../assets/vedun.jpg"

                     alt="Company photo" class="rounded-full w-full sm:w-32 h-auto sm:h-32 mb-2" />
            </div>
            <div class="w-full sm:w-auto">
                <div class="font-semibold text-xl bg-green-800 text-white p-2">
                    Рейтинг компании
                </div>
                <div class="text-sm text-gray-700 px-2">Качество услуг</div>
                <div class="flex items-center space-x-2 mt-2">
                    <span v-for="star in Array(company.quality).fill('')">&#9733;</span>
                </div>
                <div class="text-sm text-gray-700 px-2">Условия работы</div>
                <div class="flex items-center space-x-2 mt-2">
                    <span v-for="star in Array(company.conditions).fill('')">&#9733;</span>
                </div>
                <div class="text-sm text-gray-700 px-2 mt-2">Охват отраслей</div>
                <div class="flex items-center space-x-2 mt-2">
                    <span v-for="star in Array(company.industryCoverage).fill('')">&#9733;</span>
                </div>
                <div class="mt-2 px-2 border-t-2">
                    <div>Общий рейтинг: {{ calculateOverallRating(company) }}</div>
                </div>
            </div>
        </div>
    </div>


    <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50">
        <div class="bg-white p-6 rounded-lg">
            <p>В БЕТА ТЕСТЕ</p>
            <button @click="closeModal" class="mt-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Закрыть</button>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import gsap from 'gsap';

    export default {
        data() {
            return {
                companies: [],
                showModal: false
            };
        },
        mounted() {
            this.loadCompanies();
            gsap.to(this.$refs.titleWrapper, { y: -5, duration: 1, repeat: -1, yoyo: true, ease: "power1.inOut" });
        },
        methods: {
            async loadCompanies() {
                try {
                    const response = await axios.get('https://localhost:7175/api/AuthProcess/takeData');
                    this.companies = response.data;
                } catch (error) {
                    console.error("Ошибка при загрузке компаний: ", error);
                }
            },
            calculateOverallRating(company) {
                const totalQuality = company.quality;
                const totalConditions = company.conditions;
                const totalIndustryCoverage = company.industryCoverage;
                const overallRating = (totalQuality + totalConditions + totalIndustryCoverage) / 3;
                return Math.round(overallRating);
            },
            openModal() {
                this.showModal = true;
            },
            closeModal() {
                this.showModal = false;
            }

        }
    }
</script>

<style scoped>
    @tailwind base;
    @tailwind components;
    @tailwind utilities;

    .container {
        overflow-y: auto;
        max-height: 70vh;
    }

</style>
