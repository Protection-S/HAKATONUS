<<template>
    <div v-if="startups.length > 0" class="container mx-auto p-4 sm:p-6">
        <div v-for="startup in startups" :key="startup.id" class="flex flex-wrap justify-evenly items-top space-x-2 pb-2 pt-2 border-b-2">
            <div class="mb-2">
                <div class="bg-green-800 font-semibold text-white p-2">
                    {{ startup.name }}
                </div>
                <div class="p-2 max-w-screen-md">
                    {{ startup.description }}
                </div>
                <div class="flex border-b-2 p-2">
                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <Icon name="mdi:calendar" width="24" height="24" fill="black" />
                        <div>{{ startup.date }}</div>
                    </div>
                    <div class="flex border-b-2 pb-2 m-2 space-x-2">
                        <Icon name="arcticons:zoho-projects" width="24" height="24" fill="black" />
                        <div>{{ startup.sponsored ? 'Спонсировано' : 'Не спонсировано' }}</div>
                    </div>
                </div>
            </div>
            <div class="">
                <div class="bg-green-800 font-semibold text-white p-2">Категории</div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Тип проекта:</div>
                    <div>{{ startup.projectType }}</div>
                </div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Количество участников:</div>
                    <div>{{ startup.memberCount }}</div>
                </div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Срок реализации</div>
                    <div>{{ startup.realizationDate }}</div>
                </div>
            </div>
            <img src="../assets/vedun.jpg"
                 alt="user-image" class="max-w-32 h-32 max-h-32 m-2 rounded-full" />
            <div class="">
                <div class="bg-green-800 font-semibold text-white p-2">Бюджет</div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Фрутоняня:</div>
                    <div>{{ startup.budget }}</div>
                </div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Агуша:</div>
                    <div>{{ startup.budget }}</div>
                </div>
                <div class="p-2 space-x-2 flex justify-between">
                    <div class="font-semibold">Жаренные гвозди:</div>
                    <div>{{ startup.budget }}</div>
                </div>
            </div>
        </div>
        <div class="bg-green-800 font-semibold text-center text-white p-2">
            <button @click="openModal(index)">Подробная информация</button>
        </div>
    </div>
    <div v-else class="container mx-auto p-4 sm:p-6 text-center text-gray-600">
        Нет данных о стартапах
    </div>

    <div v-if="showModal" class="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50">
        <div class="bg-white p-6 rounded-lg">
            <p>В БЕТА ТЕСТЕ</p>
            <button @click="closeModal" class="mt-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Закрыть</button>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                startups: [],
                showModal: false

            };
        },
        mounted() {
            this.fetchStartups();
        },
        methods: {
            async fetchStartups() {
                try {
                    const response = await fetch('https://localhost:7175/api/AuthProcess/getAllStartups');
                    const data = await response.json();
                    this.startups = data;
                } catch (error) {
                    console.error('Ошибка при получении данных о стартапах:', error);
                }
            },
            openModal() {
                this.showModal = true;
            },
            closeModal() {
                this.showModal = false;
            }
        }
    };
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
